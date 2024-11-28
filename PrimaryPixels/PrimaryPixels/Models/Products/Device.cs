namespace PrimaryPixels.Models.Products;

public abstract class Device : Product
{
    public string Cpu { get; init; }
    public int Ram { get; init; }
    public int InternalMemory { get; init; }
}