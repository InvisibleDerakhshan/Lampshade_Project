using _0_Framework.Domain;
using System.Collections.Generic;
using System.Linq;

namespace InventoryManagement.Domain.InventoryAgg
{
    public class Inventory : EntityBase
    {

        public long ProductId { get; private set; }
        public double UnitPrice { get; private set; }
        public bool InStock { get; private set; }
        public List<InventoryOperation> Operations { get; private set; }

        public Inventory(long productId, double unitPrice)
        {
            ProductId = productId;
            UnitPrice = unitPrice;
            InStock = false;
        }


        public void Edit(long productId, double unitPrice)
        {
            ProductId = productId;
            UnitPrice = unitPrice;
        }


        public long CalculateCurrentCount()//niaz nist kasi az birron call kone
        {
            //operation = 1
            var plus = Operations.Where(x => x.Operation).Sum(x => x.Count);

            //operation =0
            var minus = Operations.Where(x => !x.Operation).Sum(x => x.Count);

            return plus - minus;
        }

        public void Increase(long count, long operatorid, string description)
        {
            var currentCount = CalculateCurrentCount() + count;

            //oder id = 0 because increase is just done by person from insidecompany not a customer
            var operation = new InventoryOperation(true, count, operatorid, currentCount, description, 0, Id);
            Operations.Add(operation);

            //if(currentCount>0)
            //    InStock = true;
            //else
            //    InStock = false;
             
            InStock = currentCount > 0; //simplifide of statement 42

        }

        public void Reduce(long count, long operatorid, string description, long oderId)
        {
            var currentCount = CalculateCurrentCount() - count;
            var operation = new InventoryOperation (false ,count ,operatorid, currentCount, description, oderId ,Id);
            Operations.Add(operation);
            InStock = currentCount > 0;
        }

    }
}
