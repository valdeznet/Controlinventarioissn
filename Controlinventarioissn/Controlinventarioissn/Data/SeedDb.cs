using Controlinventarioissn.Data.Entities;
using Controlinventarioissn.Enums;
using Controlinventarioissn.Helpers;
using Controlinventarioissn.Migrations;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Metrics;

namespace Controlinventarioissn.Data
{
    public class SeedDb
    {
        private readonly DataContext _context;
            private readonly IUserHelper _userHelper;
            private readonly IBlobHelper _blobHelper;

        public SeedDb(DataContext context, IBlobHelper blobHelper, IUserHelper userHelper)
        {
            _context = context;
            _userHelper = userHelper;
            _blobHelper = blobHelper;
        }

        public async Task SeedAsync()
        {
            await _context.Database.EnsureCreatedAsync();
            await CheckCategoriesAsync();
            await CheckDepositosAsync();
            await CheckEquipamientosAsync();
            await CheckDelegacionesAsync();
            await CheckRolesAsync();
            await CheckUserAsync("1010", "Pablo", "Valdez", "valdeznet@gmail.com", "29950000", "Minas", "Pablo.png", UserType.Admin);
            //await CheckUserAsync("2020", "Flavia", "Jordi", "tecnokraal@gmail.com", "29950000", "Minas", UserType.User);
        }
        private async Task CheckEquipamientosAsync()
        {
            if (!_context.Equipamientos.Any())
            {
              //await AddEquipamientoAsync("Auricular Gamer", "452255", 12F, new List<string>() { "Tecnologia" }, new List<string>() { "Auricular Gamer.png" });
              //  await AddEquipamientoAsync("Servidor", 4555M, 12F, new List<string>() { "Tecnologia" }, new List<string>() { "Servidor.jpg" });
                await AddEquipamientoAsync("Auricular Gamer", 980000M, 12F, new List<string>() { "Tecnología" }, new List<string>() { "Auricular Gamer.png" });
                await _context.SaveChangesAsync();
            }

        }

        private async Task AddEquipamientoAsync(string name, decimal NumeroRfid, float stock, List<string> categories, List<string> images)
        {
            Equipamiento equipamiento = new()
            {
                Description = name,
                Name = name,
                NumeroRfid = NumeroRfid,
                Stock = stock,
                EquipamientoCategories = new List<EquipamientoCategory>(),
                EquipamientoImages = new List<EquipamientoImage>()
            };

            foreach (string? category in categories)
            {
                equipamiento.EquipamientoCategories.Add(new EquipamientoCategory { Category = await _context.Categories.FirstOrDefaultAsync(c => c.Name == category) });
            }

            foreach (string? image in images)
            {
                Guid imageId = await _blobHelper.UploadBlobAsync($"{Environment.CurrentDirectory}\\wwwroot\\images\\equipamientos\\{image}", "equipamientos");
                equipamiento.EquipamientoImages.Add(new EquipamientoImage { ImageId = imageId });
            }

            _context.Equipamientos.Add(equipamiento);
        }

        private async Task<User> CheckUserAsync(
     string document,
     string firstName,
     string lastName,
     string email,
     string phone,
     string address,
     string image,
     UserType userType)
        {
            User user = await _userHelper.GetUserAsync(email);
            if (user == null)
            {
                Guid imageId = await _blobHelper.UploadBlobAsync($"{Environment.CurrentDirectory}\\wwwroot\\images\\users\\{image}", "users");

                user = new User
                {
                    FirstName = firstName,
                    LastName = lastName,
                    Email = email,
                    UserName = email,
                    PhoneNumber = phone,
                    Address = address,
                    Document = document,
                    sector = _context.Sectors.FirstOrDefault(),
                    UserType = userType,
                    ImageId = imageId
                };

                await _userHelper.AddUserAsync(user, "123456");
                await _userHelper.AddUserToRoleAsync(user, userType.ToString());
         


            }

            return user;
        }

        private async Task CheckRolesAsync()
        {
            await _userHelper.CheckRoleAsync(UserType.Admin.ToString());
            await _userHelper.CheckRoleAsync(UserType.User.ToString());

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

        private async Task CheckDepositosAsync()
        {
            if (!_context.Depositos.Any())
            {
                _context.Depositos.Add(new Deposito { Name = "SedeCentral" });
                _context.Depositos.Add(new Deposito { Name = "Jubilaciones" });
                _context.Depositos.Add(new Deposito { Name = "Turismo" });
                _context.Depositos.Add(new Deposito { Name = "Derivaciones" });

                await _context.SaveChangesAsync();
            }
        }
    }
}
