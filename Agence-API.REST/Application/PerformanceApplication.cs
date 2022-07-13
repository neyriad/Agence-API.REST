using Agence_API.REST.Domain.Adapters;
using Agence_API.REST.Domain.DataAccess;
using Agence_API.REST.Domain.EntitiesDto;
using Agence_API.REST.Domain.ModelView;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Agence_API.REST.Application
{
    public static class PerformanceApplication
    {
        /// <summary>
        /// Responsible of getting all consultants
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public static List<ConsultantDto> GetAllConsultants()
        {
            var list = new List<ConsultantDto>();
            var query = @"SELECT [dbo].[cao_usuario].[no_usuario] as Name 
                ,[dbo].[cao_usuario].[no_email] as EmailJob 
                ,[dbo].[cao_usuario].[no_email_pessoal] as EmailPrivate 
                ,[dbo].[cao_usuario].[nu_telefone] as Phone 
                ,[dbo].[cao_usuario].[co_usuario] as UserName 
                FROM [dbo].[cao_usuario] 
                LEFT JOIN [dbo].[permissao_sistema] ON [dbo].[permissao_sistema].co_usuario = [dbo].[cao_usuario].co_usuario 
                WHERE [dbo].[permissao_sistema].co_sistema = 1 AND 
                ([dbo].[permissao_sistema].in_ativo = 's' OR [dbo].[permissao_sistema].in_ativo = 'S') AND 
                ([dbo].[permissao_sistema].co_tipo_usuario = 1 OR 
                [dbo].[permissao_sistema].co_tipo_usuario = 2 OR [dbo].[permissao_sistema].co_tipo_usuario = 3)";

            using (var command = EntitiesDataAccess.Instance.Database.Connection.CreateCommand())
            {
                try
                {
                    command.CommandText = query;
                    using (var result = command.ExecuteReader())
                    {
                        while (result.Read())
                        {
                            list.Add(ConsultantAdapter.ToBusinessEntity(result));
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            return list;
        }

        /// <summary>
        /// Responsible of getting earnings by user
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public static List<EarningsDto> GetEarningsByConsultant(RequestRangeParams param)
        {
            var earningList = new List<EarningsDto>();
            var billingList = new List<BillingDto>();

            var fixedCost = EntitiesDataAccess.Instance.cao_salario
                .FirstOrDefault(x => x.co_usuario == param.User)?.brut_salario ?? 0;

            var dateInitRageFrom = param.InitDate.ToString("yyyy-MM-dd");
            var dateInitRageEnd = param.InitDate.AddMonths(1).ToString("yyyy-MM-dd");
            billingList = GetBillCommissionAndTaxes(param.User, dateInitRageFrom, dateInitRageEnd);

            earningList.Add(BuildEarningRecord(billingList, fixedCost));

            var dateEndRageFrom = param.EndDate.ToString("yyyy-MM-dd"); ;
            var dateEndRageEnd = param.EndDate.AddMonths(1).ToString("yyyy-MM-dd");
            billingList = GetBillCommissionAndTaxes(param.User, dateEndRageFrom, dateEndRageEnd);

            earningList.Add(BuildEarningRecord(billingList, fixedCost));
              
            //Adding totals
            earningList.Add(new EarningsDto
            {
                NetEarning = earningList.Sum(x => x.NetEarning),
                FixedCost = earningList.Sum(x => x.FixedCost),
                Commission = earningList.Sum(x => x.Commission),
                Profit = earningList.Sum(x => x.Profit)
            });

            return earningList;
        }

        /// <summary>
        /// Respnosible of getting net earnings by a range of month in a year
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public static PerformanceYearDto GetPerformanceByConsultantAndRange(RequestRangeParams param)
        {
            var valueList = new List<double>();
            var monthList = new List<string>();

            var fixedCost = EntitiesDataAccess.Instance.cao_salario
                .FirstOrDefault(x => x.co_usuario == param.User)?.brut_salario ?? 0;

            var dateFrom = param.InitDate.ToString("yyyy-MM-dd");
            var dateEnd = param.EndDate.AddMonths(1).ToString("yyyy-MM-dd");

            var billingList = GetBillAndTaxes(param.User, dateFrom, dateEnd);

            billingList = billingList.OrderBy(x => x.BillDate).ToList();
            for (var i = param.InitDate; i.Year <= param.EndDate.Year && i.Month <= param.EndDate.Month; i=i.AddMonths(1))
            {
                var billing = billingList.Where(x => x.BillDate.Month == i.Month && x.BillDate.Year == i.Year)
                    .Sum(x => x.BillValue - ((double)x.BillValue * x.Taxes / 100));
                monthList.Add(i.ToString("MMM-yyyy"));
                valueList.Add( Math.Round(billing, 2) );
            }

            return new PerformanceYearDto
            {
                UserName = param.User,
                MonthList = monthList,
                ValueList = valueList,
                FixedCost = fixedCost
            };
        }

        /// <summary>
        /// Respnosible of getting net earnings totals
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public static PerformanceYearDto GetPerformanceForPercentage(RequestRangeParams param)
        {
            var dateFrom = param.InitDate.ToString("yyyy-MM-dd");
            var dateEnd = param.EndDate.AddMonths(1).ToString("yyyy-MM-dd");

            var billingList = GetBillAndTaxes(param.User, dateFrom, dateEnd);
            var billing = billingList.Sum(x => x.BillValue - ((double)x.BillValue * x.Taxes / 100));

            return new PerformanceYearDto
            {
                UserName = param.User,
                TotalPerformance = Math.Round(billing, 2)
            };
        }


        #region Private region
        /// <summary>
        /// Responsible of getting billing data by user
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        private static List<BillingDto> GetBillCommissionAndTaxes(string user, string init_date, string end_date)
        {
            var query = $@"SELECT [dbo].[cao_fatura].co_fatura as BillId
		        ,[dbo].[cao_fatura].valor as BillValue
		        ,[dbo].[cao_fatura].comissao_cn as Commission
		        ,[dbo].[cao_fatura].total_imp_inc as Taxes
                FROM [dbo].[cao_os]
                LEFT JOIN [dbo].[cao_fatura] ON [dbo].[cao_fatura].co_os = [dbo].[cao_os].co_os
                WHERE [dbo].[cao_os].co_usuario = '{user}'
	            AND [dbo].[cao_fatura].data_emissao >= '{init_date}'
	            AND [dbo].[cao_fatura].data_emissao < '{end_date}'";

            return GetBillingFromQuery(query);
        }

        /// <summary>
        /// Responsible of getting billing data by user
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        private static List<BillingDto> GetBillAndTaxes(string user, string init_date, string end_date)
        {
            var query = $@"SELECT 
            [dbo].[cao_fatura].valor as BillValue
            ,[dbo].[cao_fatura].data_emissao as BillDate
            ,[dbo].[cao_fatura].total_imp_inc as Taxes
                  FROM [dbo].[cao_os]
                  LEFT JOIN [dbo].[cao_fatura] ON [dbo].[cao_fatura].co_os = [dbo].[cao_os].co_os
                  WHERE [dbo].[cao_os].co_usuario = '{user}'
               AND [dbo].[cao_fatura].data_emissao >= '{init_date}'
               AND [dbo].[cao_fatura].data_emissao < '{end_date}'";

            return GetBillingFromQuery(query);
        }

        /// <summary>
        /// Responsible of earning, commision and profit calculation
        /// </summary>
        /// <param name="bills"></param>
        /// <param name="fixedCost"></param>
        /// <returns></returns>
        private static EarningsDto BuildEarningRecord(List<BillingDto> bills, double fixedCost)
        {
            var earning = new EarningsDto
            {
                FixedCost = -1 * fixedCost
            };

            var value = bills.Sum(x => x.BillValue);
            var taxes = bills.Sum(x => (double)x.BillValue * x.Taxes / 100);
            var commission = bills.Sum(x => (double)x.BillValue * x.Commission / 100);

            earning.NetEarning = value - taxes;
            earning.Commission = (value - (value * taxes)) * commission;
            earning.Profit = (value - taxes) - (fixedCost + commission);

            return earning;
        }

        /// <summary>
        /// Responsible of execute custom queries
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        private static List<BillingDto> GetBillingFromQuery(string query)
        {
            var billingList = new List<BillingDto>();

            using (var command = EntitiesDataAccess.Instance.Database.Connection.CreateCommand())
            {
                try
                {
                    command.CommandText = query;

                    using (var result = command.ExecuteReader())
                    {
                        var fieldNames = Enumerable.Range(0, result.FieldCount).Select(i => result.GetName(i)).ToArray();
                        var containsId = fieldNames.Contains("BillId");
                        while (result.Read())
                        {
                            if (containsId)
                                billingList.Add(BillingAdapter.BusinessEntityForEarnings(result));
                            else
                                billingList.Add(BillingAdapter.BusinessEntityForPerformance(result));
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            return billingList;
        }

        #endregion
    }
}