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
        public List<Tax_and_fee> GetAllTaxes();
        public void CreateTax(CreateTaxViewModel model);
        public void UpdateTax(EditTaxViewModel model);
        public void SoftDeleteTaxes(int id);
        public List<OrderListViewModel> GetAllOrders();
        List<InvoiceDummyData> GetInvoiceItems(string orderNumber);
    }
}