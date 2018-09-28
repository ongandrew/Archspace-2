namespace Archspace2
{
    public class Resource
    {
        public long ProductionPoint { get; set; }
        public long ResearchPoint { get; set; }
        public long MilitaryPoint { get; set; }

        public Resource()
        {
            ProductionPoint = 0;
            ResearchPoint = 0;
            MilitaryPoint = 0;
        }

        public Resource(Resource aResource)
        {
            ProductionPoint = aResource.ProductionPoint;
            ResearchPoint = aResource.ResearchPoint;
            MilitaryPoint = aResource.MilitaryPoint;
        }
        
        public static Resource operator -(Resource aResource)
        {
            return new Resource()
            {
                ProductionPoint = -aResource.ProductionPoint,
                ResearchPoint = -aResource.ResearchPoint,
                MilitaryPoint = -aResource.MilitaryPoint
            };
        }

        public static Resource operator +(Resource lhs, Resource rhs)
        {
            return new Resource()
            {
                ProductionPoint = lhs.ProductionPoint + rhs.ProductionPoint,
                ResearchPoint = lhs.ResearchPoint + rhs.ResearchPoint,
                MilitaryPoint = lhs.MilitaryPoint + rhs.MilitaryPoint
            };
        }

        public static Resource operator -(Resource lhs, Resource rhs)
        {
            return lhs + (-rhs);
        }
    }
}
