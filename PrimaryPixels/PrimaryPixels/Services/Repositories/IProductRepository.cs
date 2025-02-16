using PrimaryPixels.Models.Products;

namespace PrimaryPixels.Services.Repositories;

public interface IProductRepository
{
    public Task<IEnumerable<Product>> GetAll();
    public Task<Product> GetById(int id);
    public Task<IEnumerable<Product>> GetPopular();
    public Task<IEnumerable<Product>> Search(string word);
}