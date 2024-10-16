using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;


namespace FoodHub.Models {
    public class Item
    {
        public int ItemId { get; set; }

        [RegularExpression(@"[0-9a-zA-Zæøå. \-]{2,20}", ErrorMessage = "The name must be numbers or letters and between 2 and 20 characters")]
        [Display(Name = "Item Name")]
        public string Name { get; set; } = string.Empty;

        [StringLength(200)]
        public string? Description { get; set; }
        public string? ImageUrl { get; set; }

        // Nutritional Information
        [Range(0, double.MaxValue, ErrorMessage = "Energy must be a non-negative value")]
        public decimal Energy { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Carbohydrate must be a non-negative value")]
        public decimal Carbohydrate { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Total fat must be a non-negative value")]
        public decimal TotalFat { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Saturated fat must be a non-negative value")]
        public decimal SaturatedFat { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Unsaturated fat must be a non-negative value")]
        public decimal UnsaturedFat { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Sugar must be a non-negative value")]
        public decimal Sugar { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Dietary fiber must be a non-negative value")]
        public decimal? DietaryFiber { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Protein must be a non-negative value")]
        public decimal Protein { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Salt must be a non-negative value")]
        public decimal Salt { get; set; }

    }
}