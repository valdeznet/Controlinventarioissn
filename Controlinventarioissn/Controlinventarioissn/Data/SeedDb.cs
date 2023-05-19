using Controlinventarioissn.Data.Entities;
using Controlinventarioissn.Helpers;
using System.Diagnostics.Metrics;

namespace Controlinventarioissn.Data
{
    public class SeedDb
    {
        private readonly DataContext _context;
        //   private readonly IUserHelper _userHelper;
            private readonly IBlobHelper _blobHelper;

        public SeedDb(DataContext context, IBlobHelper blobHelper)
        {
            _context = context;
            // _userHelper = userHelper;
            _blobHelper = blobHelper;
        }

        public async Task SeedAsync()
        {
            await _context.Database.EnsureCreatedAsync();
            await CheckCategoriesAsync();
            await CheckDelegacionesAsync();
        }

        private async Task CheckDelegacionesAsync()
        {
            if (!_context.Delegaciones.Any())
            {
                _context.Delegaciones.Add(new Delegacion
                {
                    Name = "Plottier",
                    Sectors = new List<Sector>()
                    {
                        new Sector()
                        {
                            Name = "Contaduria",                                                       
                        },
                        new Sector()
                        {
                            Name = "Tesoreria",                          
                        },
                        new Sector()
                        {
                            Name = "Sistemas",
                        },
                        new Sector()
                        {
                            Name = "Patrimoniales",
                        },
                        new Sector()
                        {
                            Name = "Administracion",
                        },
                        new Sector()
                        {
                            Name = "Mesa de Entrada",
                        },
                    }
                });
                _context.Delegaciones.Add(new Delegacion
                {
                    Name = "Zapala",
                    Sectors = new List<Sector>()
                    {
                        new Sector()
                        {
                            Name = "Derivaciones",

                            
                        },
                        new Sector()
                        {
                            Name = "Contaduria",                                                       
                        },
                    }
                });
            }

            await _context.SaveChangesAsync();
        }

        private async Task CheckCategoriesAsync()
        {
            if (!_context.Categories.Any())
            {
                _context.Categories.Add(new Category { Name = "Tecnología" });
                _context.Categories.Add(new Category { Name = "Construcción" });
                _context.Categories.Add(new Category { Name = "Indumentaria" });
                _context.Categories.Add(new Category { Name = "Medicacion" });
                _context.Categories.Add(new Category { Name = "Nutrición" });
                _context.Categories.Add(new Category { Name = "Herramientas" });
                _context.Categories.Add(new Category { Name = "Automotriz" });
                _context.Categories.Add(new Category { Name = "Mobiliario" });
                await _context.SaveChangesAsync();
            }
        }
    }
}
