using System.ComponentModel.DataAnnotations;

namespace SimpleStore.Model;

public enum Category
{
    Mobile = 0,
    Laptop = 1,
    Television =2,
    Fridge=3,
    Fan=4,
    AC=5

}
public class Product
{
    [Key]
    public int ProductId {get; set;}
    public string ProductName {get; set;}
    public int Quantity {get; set;}
    public Category Category{get; set;}
}
