
using Microsoft.CodeAnalysis.Differencing;
using Pizza_Shop_.Models;
using Pizza_Shop_.ViewModels;
namespace Pizza_Shop_.Repositories
{
    public interface ITableSectionRepository
    {
        List<Section> GetAllSections();
        List<Table> GetAllTables();
        void AddSection(Section section);
        void UpdateSection(EditSectionViewModel model);
        public void SoftDeleteSection(int sectionId);
        void AddTable(Table table);
        public void SoftDeleteTables(int tableId);
        public void UpdateTable(EditTableViewModel model);
        public List<Tax_and_fee> GetAllTaxes();
        public void AddTax(Tax_and_fee tax);
        public void UpdateTax(EditTaxViewModel model);
        public void SoftDeleteTaxes(int id);
    }
}