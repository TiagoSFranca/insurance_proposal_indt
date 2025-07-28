using InsuranceProposal.Common.Requests;
using InsuranceProposal.Common.Responses;
using Microsoft.EntityFrameworkCore;

namespace InsuranceProposal.Common.Helpers;

public static class PaginationHelper
{
    private static (int skip, int take) CalculateOffset(PageRequest pageRequest)
    {
        var skip = (pageRequest.Number - 1) * pageRequest.Limit;
        return (skip, pageRequest.Limit);
    }

    private static List<TEntity> PaginateEntity<TEntity>(IQueryable<TEntity> query, PageRequest pageRequest)
    {
        var (skip, take) = CalculateOffset(pageRequest);

        return query
            .Skip(skip)
            .Take(take)
            .ToList();
    }

    public static async Task<PageResponse<TEntity>> Paginate<TEntity>(IQueryable<TEntity> query, PageRequest pageRequest)
    {
        var totalItens = await query.CountAsync();

        var paginatedResult = PaginateEntity(query, pageRequest);

        var retorno = PageResponse<TEntity>.For(paginatedResult, pageRequest, totalItens);

        return retorno;
    }
}
