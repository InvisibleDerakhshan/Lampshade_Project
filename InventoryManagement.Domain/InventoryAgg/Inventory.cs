using _0_Framework.Domain;
using System;
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

        public Inventory(long productId, double unitPrice, bool inStock)
        {
            ProductId = productId;
            UnitPrice = unitPrice;
            InStock = false;
        }

        private long CalculateCurrentCount()//niaz nist kasi az birron call kone
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

    public class InventoryOperation
    {
        public long Id { get; private set; }
        public bool Operation { get; private set; }
        public long Count { get; private set; }
        public long OperationId { get; private set; }
        public DateTime OperationDate { get; private set; }
        public long CurrentCount { get; private set; }
        public string Description { get; private set; }
        public long OrderId { get; private set; }
        public long InventoryId { get; private set; }
        public Inventory Inventory { get; private set; }

        public InventoryOperation(bool operation, long count, long operationId,
            long currentCount, string description, long orderId, long inventoryId)
        {
            Operation = operation;
            Count = count;
            OperationId = operationId;
            CurrentCount = currentCount;
            Description = description;
            OrderId = orderId;
            InventoryId = inventoryId;
        }
    }
}
