using Pizza_Shop_.Models;
using Pizza_Shop_.ViewModels;
namespace Pizza_Shop_.Services
{
    public interface ITableSectionService
    {
        List<Section> GetAllSections();
        List<Table> GetAllTables();
        void AddSection(AddSectionViewModel model);
        void UpdateSection(EditSectionViewModel model);
        void SoftDeleteSection(int sectionId);
        void AddTable(TableViewModel tableViewModel);
        void SoftDeleteTables(int tableId);
        void UpdateTable(EditTableViewModel model);
    }
}