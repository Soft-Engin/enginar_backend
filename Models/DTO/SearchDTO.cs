namespace Models.DTO
{
    public class BlogSearchParams
    {
        public string? HeaderContains { get; set; }
        public string? BodyContains { get; set; }
        public string? UserName { get; set; }
        public int? RecipeId { get; set; }

        // Ingredient and Allergen filtering (IDs)
        public List<int> IngredientIds { get; set; } = new List<int>();
        public List<int> AllergenIds { get; set; } = new List<int>();

        // Sorting: Allowed fields: Header, BodyText, UserName, CreationDate
        public string SortBy { get; set; } = "Header";
        public string SortOrder { get; set; } = "asc"; // "asc" or "desc"
    }

    public class RecipeSearchParams
    {
        public string? HeaderContains { get; set; }
        public string? BodyContains { get; set; }
        public string? UserName { get; set; }

        public List<int> IngredientIds { get; set; } = new List<int>();
        public List<int> AllergenIds { get; set; } = new List<int>();

        // Sorting: Allowed fields: Header, BodyText, UserName, CreationDate
        public string SortBy { get; set; } = "Header";
        public string SortOrder { get; set; } = "asc"; // "asc" or "desc"
    }

    public class AllergenSearchParams
    {
        public string? NameContains { get; set; }
        public string? DescriptionContains { get; set; }

        // Allowed fields: Name, Description
        public string SortBy { get; set; } = "Name";
        public string SortOrder { get; set; } = "asc";
    }

    public class IngredientSearchParams
    {
        public string? NameContains { get; set; }
        public List<int> IngredientTypeIds { get; set; } = new List<int>();
        public List<int> AllergenIds { get; set; } = new List<int>();

        // Allowed fields: Name, Type
        public string SortBy { get; set; } = "Name";
        public string SortOrder { get; set; } = "asc";
    }

    public class IngredientTypeSearchParams
    {
        public string? NameContains { get; set; }
        public string? DescriptionContains { get; set; }

        // Allowed fields: Name, Description
        public string SortBy { get; set; } = "Name";
        public string SortOrder { get; set; } = "asc";
    }

    public class UserSearchParams
    {
        public string? UserNameContains { get; set; }
        public string? First_LastNameContains { get; set; }
        public string? EmailContains { get; set; }

        // Allowed fields: Name, Email, Role
        public string SortBy { get; set; } = "Name";
        public string SortOrder { get; set; } = "asc";
    }

    public class EventSearchParams
    {
        public string? TitleContains { get; set; }
        public string? BodyContains { get; set; }
        public string? CreatorUserName { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        
        // Location filters
        public List<int> CountryIds { get; set; } = new List<int>();
        public List<int> CityIds { get; set; } = new List<int>();
        public List<int> DistrictIds { get; set; } = new List<int>();
        
        // Requirements filter
        public List<int> RequirementIds { get; set; } = new List<int>();
        
        // Sorting: Allowed fields: Title, Date, CreatorUserName
        public string SortBy { get; set; } = "Date";
        public string SortOrder { get; set; } = "asc"; // "asc" or "desc"
    }
}
