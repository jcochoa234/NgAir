using Microsoft.EntityFrameworkCore;
using NgAir.BackEnd.Helpers;
using NgAir.BackEnd.UnitsOfWork.Interfaces;
using NgAir.Shared.Entities;
using NgAir.Shared.Enums;

namespace NgAir.BackEnd.Data
{
    public class SeedDb
    {
        private readonly DataContext _context;
        private readonly IUsersUnitOfWork _usersUnitOfWork;
        private readonly IFileStorage _fileStorage;

        public SeedDb(DataContext context, IUsersUnitOfWork usersUnitOfWork, IFileStorage fileStorage)
        {
            _context = context;
            _usersUnitOfWork = usersUnitOfWork;
            _fileStorage = fileStorage;
        }

        public async Task SeedAsync()
        {
            await _context.Database.EnsureCreatedAsync();
            //await CheckCountriesFullAsync();
            await CheckCountriesAsync();
            await CheckCatregoriesAsync();
            await CheckRolesAsync();
            await CheckProductsAsync();
            await CheckUserAsync("0001", "Juan", "Zuluaga", "zulu@yopmail.com", "322 311 4620", "Calle Luna Calle Sol", "JuanZuluaga.jpg", UserType.Admin);
            await CheckUserAsync("0002", "Ledys", "Bedoya", "ledys@yopmail.com", "322 311 4620", "Calle Luna Calle Sol", "LedysBedoya.jpg", UserType.User);
            await CheckUserAsync("0003", "Brad", "Pitt", "brad@yopmail.com", "322 311 4620", "Calle Luna Calle Sol", "Brad.jpg", UserType.User);
            await CheckUserAsync("0004", "Angelina", "Jolie", "angelina@yopmail.com", "322 311 4620", "Calle Luna Calle Sol", "Angelina.jpg", UserType.User);
            await CheckUserAsync("0005", "Bob", "Marley", "bob@yopmail.com", "322 311 4620", "Calle Luna Calle Sol", "bob.jpg", UserType.User);
            await CheckUserAsync("0006", "Celia", "Cruz", "celia@yopmail.com", "322 311 4620", "Calle Luna Calle Sol", "celia.jpg", UserType.Admin);
            await CheckUserAsync("0007", "Fredy", "Mercury", "fredy@yopmail.com", "322 311 4620", "Calle Luna Calle Sol", "fredy.jpg", UserType.User);
            await CheckUserAsync("0008", "Hector", "Lavoe", "hector@yopmail.com", "322 311 4620", "Calle Luna Calle Sol", "hector.jpg", UserType.User);
            await CheckUserAsync("0009", "Liv", "Taylor", "liv@yopmail.com", "322 311 4620", "Calle Luna Calle Sol", "liv.jpg", UserType.User);
            await CheckUserAsync("0010", "Otep", "Shamaya", "otep@yopmail.com", "322 311 4620", "Calle Luna Calle Sol", "otep.jpg", UserType.User);
            await CheckUserAsync("0011", "Ozzy", "Osbourne", "ozzy@yopmail.com", "322 311 4620", "Calle Luna Calle Sol", "ozzy.jpg", UserType.User);
            await CheckUserAsync("0012", "Selena", "Quintanilla", "selenba@yopmail.com", "322 311 4620", "Calle Luna Calle Sol", "selena.jpg", UserType.User);
        }

        private async Task CheckProductsAsync()
        {
            if (!_context.Products.Any())
            {
                await AddProductAsync("Adidas Barracuda", 270000M, 12F, new List<string>() { "Calzado", "Deportes" }, new List<string>() { "adidas_barracuda.png" });
                await AddProductAsync("Adidas Superstar", 250000M, 12F, new List<string>() { "Calzado", "Deportes" }, new List<string>() { "Adidas_superstar.png" });
                await AddProductAsync("Aguacate", 5000M, 500F, new List<string>() { "Comida" }, new List<string>() { "Aguacate1.png", "Aguacate2.png", "Aguacate3.png" });
                await AddProductAsync("AirPods", 1300000M, 12F, new List<string>() { "Tecnología", "Apple" }, new List<string>() { "airpos.png", "airpos2.png" });
                await AddProductAsync("Akai APC40 MKII", 2650000M, 12F, new List<string>() { "Tecnología" }, new List<string>() { "Akai1.png", "Akai2.png", "Akai3.png" });
                await AddProductAsync("Apple Watch Ultra", 4500000M, 24F, new List<string>() { "Apple", "Tecnología" }, new List<string>() { "AppleWatchUltra1.png", "AppleWatchUltra2.png" });
                await AddProductAsync("Audifonos Bose", 870000M, 12F, new List<string>() { "Tecnología" }, new List<string>() { "audifonos_bose.png" });
                await AddProductAsync("Bicicleta Ribble", 12000000M, 6F, new List<string>() { "Deportes" }, new List<string>() { "bicicleta_ribble.png" });
                await AddProductAsync("Camisa Cuadros", 56000M, 24F, new List<string>() { "Ropa" }, new List<string>() { "camisa_cuadros.png" });
                await AddProductAsync("Casco Bicicleta", 820000M, 12F, new List<string>() { "Deportes" }, new List<string>() { "casco_bicicleta.png", "casco.png" });
                await AddProductAsync("Gafas deportivas", 160000M, 24F, new List<string>() { "Deportes" }, new List<string>() { "Gafas1.png", "Gafas2.png", "Gafas3.png" });
                await AddProductAsync("Hamburguesa triple carne", 25500M, 240F, new List<string>() { "Comida" }, new List<string>() { "Hamburguesa1.png", "Hamburguesa2.png", "Hamburguesa3.png" });
                await AddProductAsync("iPad", 2300000M, 6F, new List<string>() { "Tecnología", "Apple" }, new List<string>() { "ipad.png" });
                await AddProductAsync("iPhone 13", 5200000M, 6F, new List<string>() { "Tecnología", "Apple" }, new List<string>() { "iphone13.png", "iphone13b.png", "iphone13c.png", "iphone13d.png" });
                await AddProductAsync("Johnnie Walker Blue Label 750ml", 1266700M, 18F, new List<string>() { "Licores" }, new List<string>() { "JohnnieWalker3.png", "JohnnieWalker2.png", "JohnnieWalker1.png" });
                await AddProductAsync("KOOY Disfraz inflable de gallo para montar", 150000M, 28F, new List<string>() { "Juguetes" }, new List<string>() { "KOOY1.png", "KOOY2.png", "KOOY3.png" });
                await AddProductAsync("Mac Book Pro", 12100000M, 6F, new List<string>() { "Tecnología", "Apple" }, new List<string>() { "mac_book_pro.png" });
                await AddProductAsync("Mancuernas", 370000M, 12F, new List<string>() { "Deportes" }, new List<string>() { "mancuernas.png" });
                await AddProductAsync("Mascarilla Cara", 26000M, 100F, new List<string>() { "Belleza" }, new List<string>() { "mascarilla_cara.png" });
                await AddProductAsync("New Balance 530", 180000M, 12F, new List<string>() { "Calzado", "Deportes" }, new List<string>() { "newbalance530.png" });
                await AddProductAsync("New Balance 565", 179000M, 12F, new List<string>() { "Calzado", "Deportes" }, new List<string>() { "newbalance565.png" });
                await AddProductAsync("Nike Air", 233000M, 12F, new List<string>() { "Calzado", "Deportes" }, new List<string>() { "nike_air.png" });
                await AddProductAsync("Nike Zoom", 249900M, 12F, new List<string>() { "Calzado", "Deportes" }, new List<string>() { "nike_zoom.png" });
                await AddProductAsync("Buso Adidas Mujer", 134000M, 12F, new List<string>() { "Ropa", "Deportes" }, new List<string>() { "buso_adidas.png" });
                await AddProductAsync("Suplemento Boots Original", 15600M, 12F, new List<string>() { "Nutrición" }, new List<string>() { "Boost_Original.png" });
                await AddProductAsync("Whey Protein", 252000M, 12F, new List<string>() { "Nutrición" }, new List<string>() { "whey_protein.png" });
                await AddProductAsync("Arnes Mascota", 25000M, 12F, new List<string>() { "Mascotas" }, new List<string>() { "arnes_mascota.png" });
                await AddProductAsync("Cama Mascota", 99000M, 12F, new List<string>() { "Mascotas" }, new List<string>() { "cama_mascota.png" });
                await AddProductAsync("Teclado Gamer", 67000M, 12F, new List<string>() { "Gamer", "Tecnología" }, new List<string>() { "teclado_gamer.png" });
                await AddProductAsync("Ring de Lujo 17", 1600000M, 33F, new List<string>() { "Autos" }, new List<string>() { "Ring1.png", "Ring2.png" });
                await AddProductAsync("Silla Gamer", 980000M, 12F, new List<string>() { "Gamer", "Tecnología" }, new List<string>() { "silla_gamer.png" });
                await AddProductAsync("Mouse Gamer", 132000M, 12F, new List<string>() { "Gamer", "Tecnología" }, new List<string>() { "mouse_gamer.png" });
                await _context.SaveChangesAsync();
            }
        }

        private async Task AddProductAsync(string name, decimal price, float stock, List<string> categories, List<string> images)
        {
            Product prodcut = new()
            {
                Description = name,
                Name = name,
                Price = price,
                Stock = stock,
                ProductCategories = new List<ProductCategory>(),
                ProductImages = new List<ProductImage>()
            };

            foreach (var categoryName in categories)
            {
                var category = await _context.Categories.FirstOrDefaultAsync(c => c.Name == categoryName);
                if (category != null)
                {
                    prodcut.ProductCategories.Add(new ProductCategory { Category = category });
                }
            }

            foreach (string? image in images)
            {
                var filePath = $"{Environment.CurrentDirectory}\\Images\\products\\{image}";
                var fileBytes = File.ReadAllBytes(filePath);
                var imagePath = await _fileStorage.SaveFileAsync(fileBytes, "jpg", "products");
                prodcut.ProductImages.Add(new ProductImage { Image = imagePath });
            }

            _context.Products.Add(prodcut);
        }

        private async Task CheckCountriesFullAsync()
        {
            if (!_context.Countries.Any())
            {
                var countriesStatesCitiesSQLScript = File.ReadAllText("Data\\CountriesStatesCities.sql");
                await _context.Database.ExecuteSqlRawAsync(countriesStatesCitiesSQLScript);
            }
        }

        private async Task CheckRolesAsync()
        {
            await _usersUnitOfWork.CheckRoleAsync(UserType.Admin.ToString());
            await _usersUnitOfWork.CheckRoleAsync(UserType.User.ToString());
        }

        private async Task<User> CheckUserAsync(string document, string firstName, string lastName, string email, string phone, string address, string image, UserType userType)
        {
            var user = await _usersUnitOfWork.GetUserAsync(email);
            if (user == null)
            {
                var city = await _context.Cities.FirstOrDefaultAsync(x => x.Name == "Medellín");
                city ??= await _context.Cities.FirstOrDefaultAsync();

                var filePath = $"{Environment.CurrentDirectory}\\Images\\users\\{image}";
                var fileBytes = File.ReadAllBytes(filePath);
                var imagePath = await _fileStorage.SaveFileAsync(fileBytes, "jpg", "users");

                user = new User
                {
                    FirstName = firstName,
                    LastName = lastName,
                    Email = email,
                    UserName = email,
                    PhoneNumber = phone,
                    Address = address,
                    Document = document,
                    City = city,
                    UserType = userType,
                    Photo = imagePath,
                };

                await _usersUnitOfWork.AddUserAsync(user, "123456");
                await _usersUnitOfWork.AddUserToRoleAsync(user, userType.ToString());

                var token = await _usersUnitOfWork.GenerateEmailConfirmationTokenAsync(user);
                await _usersUnitOfWork.ConfirmEmailAsync(user, token);
            }

            return user;
        }

        private async Task CheckCatregoriesAsync()
        {
            if (!_context.Categories.Any())
            {
                _context.Categories.Add(new Category { Name = "Apple" });
                _context.Categories.Add(new Category { Name = "Autos" });
                _context.Categories.Add(new Category { Name = "Belleza" });
                _context.Categories.Add(new Category { Name = "Calzado" });
                _context.Categories.Add(new Category { Name = "Comida" });
                _context.Categories.Add(new Category { Name = "Cosmeticos" });
                _context.Categories.Add(new Category { Name = "Deportes" });
                _context.Categories.Add(new Category { Name = "Erótica" });
                _context.Categories.Add(new Category { Name = "Ferreteria" });
                _context.Categories.Add(new Category { Name = "Gamer" });
                _context.Categories.Add(new Category { Name = "Hogar" });
                _context.Categories.Add(new Category { Name = "Jardín" });
                _context.Categories.Add(new Category { Name = "Jugetes" });
                _context.Categories.Add(new Category { Name = "Lenceria" });
                _context.Categories.Add(new Category { Name = "Mascotas" });
                _context.Categories.Add(new Category { Name = "Nutrición" });
                _context.Categories.Add(new Category { Name = "Ropa" });
                _context.Categories.Add(new Category { Name = "Tecnología" });
                await _context.SaveChangesAsync();
            }
        }

        private async Task CheckCountriesAsync()
        {
            if (!_context.Countries.Any())
            {
                _ = _context.Countries.Add(new Country
                {
                    Name = "Colombia",
                    States =
                    [
                        new State()
                        {
                            Name = "Antioquia",
                            Cities = [
                                new() { Name = "Medellín" },
                                new() { Name = "Itagüí" },
                                new() { Name = "Envigado" },
                                new() { Name = "Bello" },
                                new() { Name = "Rionegro" },
                                new() { Name = "Marinilla" },
                            ]
                        },
                        new State()
                        {
                            Name = "Bogotá",
                            Cities = [
                                new() { Name = "Usaquen 1" },
                                new() { Name = "Champinero 2" },
                                new() { Name = "Santa fe 3" },
                                new() { Name = "Useme 4" },
                                new() { Name = "Bosa 5" },
                                new() { Name = "Usaquen 6" },
                                new() { Name = "Champinero 7" },
                                new() { Name = "Santa fe 8" },
                                new() { Name = "Useme 9" },
                                new() { Name = "Bosa 10" },
                                new() { Name = "Usaquen 11" },
                                new() { Name = "Champinero 12" },
                                new() { Name = "Santa fe 13" },
                                new() { Name = "Useme 14" },
                                new() { Name = "Bosa 15" },
                            ]
                        },
                         new State()
                        {
                            Name = "Bogotá 1",
                            Cities = [
                                new() { Name = "Usaquen 11" },
                                new() { Name = "Champinero 21" },
                                new() { Name = "Santa fe 31" },
                                new() { Name = "Useme 41" },
                                new() { Name = "Bosa 51" },
                                new() { Name = "Usaquen 61" },
                                new() { Name = "Champinero 71" },
                                new() { Name = "Santa fe 81" },
                                new() { Name = "Useme 91" },
                                new() { Name = "Bosa 101" },
                                new() { Name = "Usaquen 111" },
                                new() { Name = "Champinero 121" },
                                new() { Name = "Santa fe 131" },
                                new() { Name = "Useme 141" },
                                new() { Name = "Bosa 151" },
                            ]
                        }, new State()
                        {
                            Name = "Bogotá 2",
                            Cities = [
                                new() { Name = "Usaquen 12" },
                                new() { Name = "Champinero 22" },
                                new() { Name = "Santa fe 32" },
                                new() { Name = "Useme 42" },
                                new() { Name = "Bosa 52" },
                                new() { Name = "Usaquen 62" },
                                new() { Name = "Champinero 72" },
                                new() { Name = "Santa fe 82" },
                                new() { Name = "Useme 92" },
                                new() { Name = "Bosa 102" },
                                new() { Name = "Usaquen 112" },
                                new() { Name = "Champinero 122" },
                                new() { Name = "Santa fe 132" },
                                new() { Name = "Useme 142" },
                                new() { Name = "Bosa 152" },
                            ]
                        }, new State()
                        {
                            Name = "Bogotá 3",
                            Cities = [
                                new() { Name = "Usaquen 13" },
                                new() { Name = "Champinero 23" },
                                new() { Name = "Santa fe 33" },
                                new() { Name = "Useme 43" },
                                new() { Name = "Bosa 53" },
                                new() { Name = "Usaquen 63" },
                                new() { Name = "Champinero 73" },
                                new() { Name = "Santa fe 83" },
                                new() { Name = "Useme 93" },
                                new() { Name = "Bosa 103" },
                                new() { Name = "Usaquen 113" },
                                new() { Name = "Champinero 123" },
                                new() { Name = "Santa fe 133" },
                                new() { Name = "Useme 143" },
                                new() { Name = "Bosa 153" },
                            ]
                        }, new State()
                        {
                            Name = "Bogotá 4",
                            Cities = [
                                new() { Name = "Usaquen 14" },
                                new() { Name = "Champinero 24" },
                                new() { Name = "Santa fe 34" },
                                new() { Name = "Useme 44" },
                                new() { Name = "Bosa 54" },
                                new() { Name = "Usaquen 64" },
                                new() { Name = "Champinero 74" },
                                new() { Name = "Santa fe 84" },
                                new() { Name = "Useme 94" },
                                new() { Name = "Bosa 104" },
                                new() { Name = "Usaquen 114" },
                                new() { Name = "Champinero 124" },
                                new() { Name = "Santa fe 134" },
                                new() { Name = "Useme 144" },
                                new() { Name = "Bosa 154" },
                            ]
                        }, new State()
                        {
                            Name = "Bogotá 5",
                            Cities = [
                                new() { Name = "Usaquen 15" },
                                new() { Name = "Champinero 25" },
                                new() { Name = "Santa fe 35" },
                                new() { Name = "Useme 45" },
                                new() { Name = "Bosa 55" },
                                new() { Name = "Usaquen 65" },
                                new() { Name = "Champinero 75" },
                                new() { Name = "Santa fe 85" },
                                new() { Name = "Useme 95" },
                                new() { Name = "Bosa 105" },
                                new() { Name = "Usaquen 115" },
                                new() { Name = "Champinero 125" },
                                new() { Name = "Santa fe 135" },
                                new() { Name = "Useme 145" },
                                new() { Name = "Bosa 155" },
                            ]
                        }, new State()
                        {
                            Name = "Bogotá 6",
                            Cities = [
                                new() { Name = "Usaquen 16" },
                                new() { Name = "Champinero 26" },
                                new() { Name = "Santa fe 36" },
                                new() { Name = "Useme 46" },
                                new() { Name = "Bosa 56" },
                                new() { Name = "Usaquen 66" },
                                new() { Name = "Champinero 76" },
                                new() { Name = "Santa fe 86" },
                                new() { Name = "Useme 96" },
                                new() { Name = "Bosa 106" },
                                new() { Name = "Usaquen 116" },
                                new() { Name = "Champinero 126" },
                                new() { Name = "Santa fe 136" },
                                new() { Name = "Useme 146" },
                                new() { Name = "Bosa 156" },
                            ]
                        }, new State()
                        {
                            Name = "Bogotá 7",
                            Cities = [
                                new() { Name = "Usaquen 17" },
                                new() { Name = "Champinero 27" },
                                new() { Name = "Santa fe 37" },
                                new() { Name = "Useme 47" },
                                new() { Name = "Bosa 57" },
                                new() { Name = "Usaquen 67" },
                                new() { Name = "Champinero 77" },
                                new() { Name = "Santa fe 87" },
                                new() { Name = "Useme 97" },
                                new() { Name = "Bosa 107" },
                                new() { Name = "Usaquen 117" },
                                new() { Name = "Champinero 127" },
                                new() { Name = "Santa fe 137" },
                                new() { Name = "Useme 147" },
                                new() { Name = "Bosa 157" },
                            ]
                        }, new State()
                        {
                            Name = "Bogotá 8",
                            Cities = [
                                new() { Name = "Usaquen 18" },
                                new() { Name = "Champinero 28" },
                                new() { Name = "Santa fe 38" },
                                new() { Name = "Useme 48" },
                                new() { Name = "Bosa 58" },
                                new() { Name = "Usaquen 68" },
                                new() { Name = "Champinero 78" },
                                new() { Name = "Santa fe 88" },
                                new() { Name = "Useme 98" },
                                new() { Name = "Bosa 108" },
                                new() { Name = "Usaquen 118" },
                                new() { Name = "Champinero 128" },
                                new() { Name = "Santa fe 138" },
                                new() { Name = "Useme 148" },
                                new() { Name = "Bosa 158" },
                            ]
                        }, new State()
                        {
                            Name = "Bogotá 9",
                            Cities = [
                                new() { Name = "Usaquen 19" },
                                new() { Name = "Champinero 29" },
                                new() { Name = "Santa fe 39" },
                                new() { Name = "Useme 49" },
                                new() { Name = "Bosa 59" },
                                new() { Name = "Usaquen 69" },
                                new() { Name = "Champinero 79" },
                                new() { Name = "Santa fe 89" },
                                new() { Name = "Useme 99" },
                                new() { Name = "Bosa 109" },
                                new() { Name = "Usaquen 119" },
                                new() { Name = "Champinero 129" },
                                new() { Name = "Santa fe 139" },
                                new() { Name = "Useme 149" },
                                new() { Name = "Bosa 159" },
                            ]
                        }, new State()
                        {
                            Name = "Bogotá 10",
                            Cities = [
                                new() { Name = "Usaquen 110" },
                                new() { Name = "Champinero 210" },
                                new() { Name = "Santa fe 310" },
                                new() { Name = "Useme 410" },
                                new() { Name = "Bosa 510" },
                                new() { Name = "Usaquen 610" },
                                new() { Name = "Champinero 710" },
                                new() { Name = "Santa fe 810" },
                                new() { Name = "Useme 910" },
                                new() { Name = "Bosa 1010" },
                                new() { Name = "Usaquen 1110" },
                                new() { Name = "Champinero 1210" },
                                new() { Name = "Santa fe 1310" },
                                new() { Name = "Useme 1410" },
                                new() { Name = "Bosa 1510" },
                            ]
                        }, new State()
                        {
                            Name = "Bogotá 11",
                            Cities = [
                                new() { Name = "Usaquen 111" },
                                new() { Name = "Champinero 211" },
                                new() { Name = "Santa fe 311" },
                                new() { Name = "Useme 411" },
                                new() { Name = "Bosa 511" },
                                new() { Name = "Usaquen 611" },
                                new() { Name = "Champinero 711" },
                                new() { Name = "Santa fe 811" },
                                new() { Name = "Useme 911" },
                                new() { Name = "Bosa 1011" },
                                new() { Name = "Usaquen 1111" },
                                new() { Name = "Champinero 1211" },
                                new() { Name = "Santa fe 1311" },
                                new() { Name = "Useme 1411" },
                                new() { Name = "Bosa 1511" },
                            ]
                        }, new State()
                        {
                            Name = "Bogotá 12",
                            Cities = [
                                new() { Name = "Usaquen 112" },
                                new() { Name = "Champinero 212" },
                                new() { Name = "Santa fe 312" },
                                new() { Name = "Useme 412" },
                                new() { Name = "Bosa 512" },
                                new() { Name = "Usaquen 612" },
                                new() { Name = "Champinero 712" },
                                new() { Name = "Santa fe 812" },
                                new() { Name = "Useme 912" },
                                new() { Name = "Bosa 1012" },
                                new() { Name = "Usaquen 1112" },
                                new() { Name = "Champinero 1212" },
                                new() { Name = "Santa fe 1312" },
                                new() { Name = "Useme 1412" },
                                new() { Name = "Bosa 1512" },
                            ]
                        },
                    ]
                });
                _context.Countries.Add(new Country
                {
                    Name = "Estados Unidos",
                    States =
                    [
                        new State()
                        {
                            Name = "Florida",
                            Cities = [
                                new() { Name = "Orlando" },
                                new() { Name = "Miami" },
                                new() { Name = "Tampa" },
                                new() { Name = "Fort Lauderdale" },
                                new() { Name = "Key West" },
                            ]
                        },
                        new State()
                        {
                            Name = "Texas",
                            Cities = [
                                new() { Name = "Houston" },
                                new() { Name = "San Antonio" },
                                new() { Name = "Dallas" },
                                new() { Name = "Austin" },
                                new() { Name = "El Paso" },
                            ]
                        },
                    ]
                });
                _context.Countries.Add(new Country
                {
                    Name = "Estados Unidos 1",
                    States =
                    [
                        new State()
                        {
                            Name = "Florida1",
                            Cities = [
                                new() { Name = "Orlando1" },
                                new() { Name = "Miami1" },
                                new() { Name = "Tampa1" },
                                new() { Name = "Fort Lauderdale1" },
                                new() { Name = "Key West1" },
                            ]
                        },
                        new State()
                        {
                            Name = "Texas1",
                            Cities = [
                                new() { Name = "Houston1" },
                                new() { Name = "San Antonio1" },
                                new() { Name = "Dallas1" },
                                new() { Name = "Austin1" },
                                new() { Name = "El Paso1" },
                            ]
                        },
                    ]
                });
                _context.Countries.Add(new Country
                {
                    Name = "Estados Unidos 2",
                    States =
                    [
                        new State()
                        {
                            Name = "Florida2",
                            Cities = [
                                new() { Name = "Orlando2" },
                                new() { Name = "Miami2" },
                                new() { Name = "Tampa2" },
                                new() { Name = "Fort Lauderdale2" },
                                new() { Name = "Key West2" },
                            ]
                        },
                        new State()
                        {
                            Name = "Texas2",
                            Cities = [
                                new() { Name = "Houston2" },
                                new() { Name = "San Antonio2" },
                                new() { Name = "Dallas2" },
                                new() { Name = "Austin2" },
                                new() { Name = "El Paso2" },
                            ]
                        },
                    ]
                });
                _context.Countries.Add(new Country
                {
                    Name = "Estados Unidos 3",
                    States =
                    [
                        new State()
                        {
                            Name = "Florida3",
                            Cities = [
                                new() { Name = "Orlando3" },
                                new() { Name = "Miami3" },
                                new() { Name = "Tampa3" },
                                new() { Name = "Fort Lauderdale3" },
                                new() { Name = "Key West3" },
                            ]
                        },
                        new State()
                        {
                            Name = "Texas3",
                            Cities = [
                                new() { Name = "Houston" },
                                new() { Name = "San Antonio" },
                                new() { Name = "Dallas" },
                                new() { Name = "Austin" },
                                new() { Name = "El Paso" },
                            ]
                        },
                    ]
                });
                _context.Countries.Add(new Country
                {
                    Name = "Estados Unidos 4",
                    States =
                    [
                        new State()
                        {
                            Name = "Florida4",
                            Cities = [
                                new() { Name = "Orlando4" },
                                new() { Name = "Miami4" },
                                new() { Name = "Tampa4" },
                                new() { Name = "Fort Lauderdale4" },
                                new() { Name = "Key West4" },
                            ]
                        },
                        new State()
                        {
                            Name = "Texas4",
                            Cities = [
                                new() { Name = "Houston4" },
                                new() { Name = "San Antonio4" },
                                new() { Name = "Dallas4" },
                                new() { Name = "Austin4" },
                                new() { Name = "El Paso4" },
                            ]
                        },
                    ]
                });
                _context.Countries.Add(new Country
                {
                    Name = "Estados Unidos 5",
                    States =
                    [
                        new State()
                        {
                            Name = "Florida5",
                            Cities = [
                                new() { Name = "Orlando5" },
                                new() { Name = "Miami5" },
                                new() { Name = "Tampa5" },
                                new() { Name = "Fort Lauderdale5" },
                                new() { Name = "Key West5" },
                            ]
                        },
                        new State()
                        {
                            Name = "Texas5",
                            Cities = [
                                new() { Name = "Houston5" },
                                new() { Name = "San Antonio5" },
                                new() { Name = "Dallas5" },
                                new() { Name = "Austin5" },
                                new() { Name = "El Paso5" },
                            ]
                        },
                    ]
                });
                _context.Countries.Add(new Country
                {
                    Name = "Estados Unidos 6",
                    States =
                    [
                        new State()
                        {
                            Name = "Florida6",
                            Cities = [
                                new() { Name = "Orlando6" },
                                new() { Name = "Miami6" },
                                new() { Name = "Tampa6" },
                                new() { Name = "Fort Lauderdale6" },
                                new() { Name = "Key West6" },
                            ]
                        },
                        new State()
                        {
                            Name = "Texas6",
                            Cities = [
                                new() { Name = "Houston6" },
                                new() { Name = "San Antonio6" },
                                new() { Name = "Dallas6" },
                                new() { Name = "Austin6" },
                                new() { Name = "El Paso6" },
                            ]
                        },
                    ]
                });
                _context.Countries.Add(new Country
                {
                    Name = "Estados Unidos 7",
                    States =
                    [
                        new State()
                        {
                            Name = "Florida7",
                            Cities = [
                                new() { Name = "Orlando7" },
                                new() { Name = "Miami7" },
                                new() { Name = "Tampa7" },
                                new() { Name = "Fort Lauderdale7" },
                                new() { Name = "Key West7" },
                            ]
                        },
                        new State()
                        {
                            Name = "Texas7",
                            Cities = [
                                new() { Name = "Houston7" },
                                new() { Name = "San Antonio7" },
                                new() { Name = "Dallas7" },
                                new() { Name = "Austin7" },
                                new() { Name = "El Paso7" },
                            ]
                        },
                    ]
                });
                _context.Countries.Add(new Country
                {
                    Name = "Estados Unidos 8",
                    States =
                    [
                        new State()
                        {
                            Name = "Florida8",
                            Cities = [
                                new() { Name = "Orlando8" },
                                new() { Name = "Miami8" },
                                new() { Name = "Tampa8" },
                                new() { Name = "Fort Lauderdale8" },
                                new() { Name = "Key West8" },
                            ]
                        },
                        new State()
                        {
                            Name = "Texas8",
                            Cities = [
                                new() { Name = "Houston8" },
                                new() { Name = "San Antonio8" },
                                new() { Name = "Dallas8" },
                                new() { Name = "Austin8" },
                                new() { Name = "El Paso8" },
                            ]
                        },
                    ]
                });
                _context.Countries.Add(new Country
                {
                    Name = "Estados Unidos 9",
                    States =
                    [
                        new State()
                        {
                            Name = "Florida9",
                            Cities = [
                                new() { Name = "Orlando9" },
                                new() { Name = "Miami9" },
                                new() { Name = "Tampa9" },
                                new() { Name = "Fort Lauderdale9" },
                                new() { Name = "Key West9" },
                            ]
                        },
                        new State()
                        {
                            Name = "Texas9",
                            Cities = [
                                new() { Name = "Houston9" },
                                new() { Name = "San Antonio9" },
                                new() { Name = "Dallas9" },
                                new() { Name = "Austin9" },
                                new() { Name = "El Paso9" },
                            ]
                        },
                    ]
                });
                _context.Countries.Add(new Country
                {
                    Name = "Estados Unidos 10",
                    States =
                    [
                        new State()
                        {
                            Name = "Florida10",
                            Cities = [
                                new() { Name = "Orlando10" },
                                new() { Name = "Miami10" },
                                new() { Name = "Tampa10" },
                                new() { Name = "Fort Lauderdale10" },
                                new() { Name = "Key West10" },
                            ]
                        },
                        new State()
                        {
                            Name = "Texas10",
                            Cities = [
                                new() { Name = "Houston10" },
                                new() { Name = "San Antonio10" },
                                new() { Name = "Dallas10" },
                                new() { Name = "Austin10" },
                                new() { Name = "El Paso10" },
                            ]
                        },
                    ]
                });
                _context.Countries.Add(new Country
                {
                    Name = "Estados Unidos 11",
                    States =
                    [
                        new State()
                        {
                            Name = "Florida11",
                            Cities = [
                                new() { Name = "Orlando11" },
                                new() { Name = "Miami11" },
                                new() { Name = "Tampa11" },
                                new() { Name = "Fort Lauderdale11" },
                                new() { Name = "Key West11" },
                            ]
                        },
                        new State()
                        {
                            Name = "Texas11",
                            Cities = [
                                new() { Name = "Houston11" },
                                new() { Name = "San Antonio11" },
                                new() { Name = "Dallas11" },
                                new() { Name = "Austin11" },
                                new() { Name = "El Paso11" },
                            ]
                        },
                    ]
                });
                _context.Countries.Add(new Country
                {
                    Name = "Estados Unidos 12",
                    States =
                    [
                        new State()
                        {
                            Name = "Florida12",
                            Cities = [
                                new() { Name = "Orlando12" },
                                new() { Name = "Miami12" },
                                new() { Name = "Tampa12" },
                                new() { Name = "Fort Lauderdale12" },
                                new() { Name = "Key West12" },
                            ]
                        },
                        new State()
                        {
                            Name = "Texas12",
                            Cities = [
                                new() { Name = "Houston12" },
                                new() { Name = "San Antonio12" },
                                new() { Name = "Dallas12" },
                                new() { Name = "Austin12" },
                                new() { Name = "El Paso12" },
                            ]
                        },
                    ]
                });
            }

            await _context.SaveChangesAsync();
        }
    }
}
