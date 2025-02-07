using PrimaryPixels.Models.ShoppingCartItem;

namespace PrimaryPixels.Services.Repositories;

public interface IShoppingCartItemRepository
{
    public Task<IEnumerable<ShoppingCartItem>> GetAll();
    public Task<ShoppingCartItem> GetById(int id);
    public Task<int> Add(ShoppingCartItem entity);
    public Task<int> Update(ShoppingCartItem entity);
    public Task<int> DeleteById(int id);
    public Task<IEnumerable<ShoppingCartItem>> GetByUserId(string id);
    public Task<bool> DeleteByUserId(string id);
}