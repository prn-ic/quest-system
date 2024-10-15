using System.Linq.Expressions;
using QuestSystem.Core.Common.Specifications;

namespace QuestSystem.Application.Common.Specifications;

public class BlankSpecification<T> : Specification<T>
{
    public override Expression<Func<T, bool>> ToExpression() => x => true;
}
