namespace CocktailBar
{
    public class Menu
    {
        public Menu(int barCapacity)
        {
            Cocktails = new List<Cocktail>();
            BarCapacity = barCapacity;
        }
        public List<Cocktail> Cocktails { get; }
        public int BarCapacity { get; set; }

        public void AddCocktail(Cocktail cocktail)
        {
            if (Cocktails.Count >= BarCapacity || Cocktails.Any(c => c.Name == cocktail.Name))
            {
               return;
            }
            Cocktails.Add(cocktail);
        }

        public bool RemoveCocktail(string name)
        {
            return Cocktails.RemoveAll(c => c.Name == name) > 0;
        }

        public Cocktail GetMostDiverse()
        {
            return Cocktails.MaxBy(c => c.Ingredients.Count);
        }

        public string Details(string name)
        {
            return Cocktails.Single(c  => c.Name == name).ToString();
            
        }

        public string GetAll()
        {
            var sortedCocktails = Cocktails.OrderBy(c => c.Name).Select(c => c.Name);
            return "All Cocktails:\n" + string.Join("\n", sortedCocktails);

        }
    }

}
