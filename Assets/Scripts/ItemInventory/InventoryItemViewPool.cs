using System;
using DependencyInjection;
using ItemInventory.UI;

namespace Pool
{
    public class InventoryItemViewPool : PoolBase<InventoryItemView>
    {
        private DependencyContainer _di;

        [Inject]
        public void Construct(DependencyContainer container)
        {
            _di = container;
        }

        public override InventoryItemView Spawn(Action<InventoryItemView> callbackBeforeAwake = null)
        {
            var view = base.Spawn(x => _di.Inject(x));
            return view;
        }

    }
}