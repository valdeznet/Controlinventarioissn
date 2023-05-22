using Controlinventarioissn.Data.Entities;
using Controlinventarioissn.Enums;
using Controlinventarioissn.Helpers;
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
            await CheckDelegacionesAsync();
            await CheckRolesAsync();
            await CheckUserAsync("1010", "Pablo", "Valdez", "valdeznet@gmail.com", "29950000", "Minas", UserType.Admin);
            await CheckUserAsync("2020", "Flavia", "Jordi", "tecnokraal@gmail.com", "29950000", "Minas", UserType.User);
        }

        private async Task<User> CheckUserAsync(
     string document,
     string firstName,
     string lastName,
     string email,
     string phone,
     string address,
     UserType userType)
        {
            User user = await _userHelper.GetUserAsync(email);
            if (user == null)
            {
     //           Guid imageId = await _blobHelper.UploadBlobAsync($"{Environment.CurrentDirectory}\\wwwroot\\images\\users\\{image}", "users");

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
       //             ImageId = imageId
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
