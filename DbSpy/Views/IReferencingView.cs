using DbSpy.ViewModels;

namespace DbSpy.Views
{
    public interface IReferencingView
    {
        DbObjectRelationshipsViewModel DbObjectRelationshipsViewModel { get; }
    }
}
