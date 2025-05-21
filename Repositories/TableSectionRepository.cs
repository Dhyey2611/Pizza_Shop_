using System.Runtime.CompilerServices;
using Microsoft.EntityFrameworkCore.Metadata;
using Pizza_Shop_.Data;
using Pizza_Shop_.Models;
using Pizza_Shop_.ViewModels;
namespace Pizza_Shop_.Repositories
{
    public class TableSectionRepository : ITableSectionRepository
    {
        private readonly DatabaseContext _context;
        public TableSectionRepository(DatabaseContext context)
        {
            _context = context;
        }
        public List<Section> GetAllSections()
        {
            return _context.Sections.Where(s => s.IsActive).ToList();
        }

        public List<Table> GetAllTables()
        {
            return _context.Tables.Where(t => t.IsDeleted == false).ToList();
        }
        public void AddSection(Section section)
        {
            section.IsActive = true;
            _context.Sections.Add(section);
            _context.SaveChanges();
        }

        public void UpdateSection(EditSectionViewModel model)
        {
            var section = _context.Sections.FirstOrDefault(s => s.SectionId == model.Id && s.IsActive == true);
            if (section != null)
            {
                section.Name = model.Name;
                section.Description = model.Description;
                _context.SaveChanges();
            }
        }
        public void SoftDeleteSection(int sectionId)
        {
            var section = _context.Sections.FirstOrDefault(s => s.SectionId == sectionId && s.IsActive == true);
            if (section != null)
            {
                section.IsActive = false;
                _context.SaveChanges();
            }
        }
        public void AddTable(Table table)
        {
            _context.Tables.Add(table);
            _context.SaveChanges();
        }
        public void SoftDeleteTables(int tableId)
        {
            var table = _context.Tables.FirstOrDefault(sd => sd.TableId == tableId && sd.IsDeleted == false);
            {
                if (table != null)
                {
                    table.IsDeleted = true;
                    _context.SaveChanges();
                }
            }
        }
        public void UpdateTable(EditTableViewModel model)
        {
            var table = _context.Tables.FirstOrDefault(t => t.TableId == model.Id && t.IsDeleted == false);
            if (table != null)
            {
                table.Table_number = model.Table_number;
                table.Capacity = model.Capacity;
                table.SectionId = model.SectionId;
                table.Status = model.Status;
                _context.SaveChanges();
            }
        }
    }
}