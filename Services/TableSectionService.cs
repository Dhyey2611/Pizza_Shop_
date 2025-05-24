using Pizza_Shop_.Models;
using Pizza_Shop_.Repositories;
using Pizza_Shop_.ViewModels;
namespace Pizza_Shop_.Services
{
    public class TableSectionService : ITableSectionService
    {
        private readonly ITableSectionRepository _tableSectionRepository;
        public TableSectionService(ITableSectionRepository tableSectionRepository)
        {
            _tableSectionRepository = tableSectionRepository;
        }
        public List<Section> GetAllSections()
        {
            return _tableSectionRepository.GetAllSections();
        }

        public List<Table> GetAllTables()
        {
            return _tableSectionRepository.GetAllTables();
        }
        public void AddSection(AddSectionViewModel model)
        {
            var section = new Section
            {
                Name = model.Name,
                Description = model.Description,
                IsActive = true
            };
            _tableSectionRepository.AddSection(section);
        }
        public void UpdateSection(EditSectionViewModel model)
        {
            _tableSectionRepository.UpdateSection(model);
        }
        public void SoftDeleteSection(int sectionId)
        {
            _tableSectionRepository.SoftDeleteSection(sectionId);
        }
        public void AddTable(TableViewModel tableViewModel)
        {
            var table = new Table
            {
                Table_number = tableViewModel.Table_number,
                Capacity = tableViewModel.Capacity,
                Status = tableViewModel.Status,
                SectionId = tableViewModel.SectionId,
            };
            _tableSectionRepository.AddTable(table);
        }
        public void SoftDeleteTables(int tableId)
        {
            _tableSectionRepository.SoftDeleteTables(tableId);
        }
        public void UpdateTable(EditTableViewModel model)
        {
            _tableSectionRepository.UpdateTable(model);
        }
        public List<Tax_and_fee> GetAllTaxes()
        {
            return _tableSectionRepository.GetAllTaxes();
        }
        public void CreateTax(CreateTaxViewModel model)
        {
            var tax = new Tax_and_fee
            {
                Name = model.Name,
                Type = model.Type,
                Value = model.Value,
                Is_enabled = model.Is_enabled,
                Is_default = model.Is_default,
                Is_active = true,
            };
            _tableSectionRepository.AddTax(tax);
        }
        public void UpdateTax(EditTaxViewModel model)
        {
            _tableSectionRepository.UpdateTax(model);
        }
        public void SoftDeleteTaxes(int id)
        {
            _tableSectionRepository.SoftDeleteTaxes(id);
        }
    }
}