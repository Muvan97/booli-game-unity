using System.Linq;
using Logic.Observers;
using UnityEngine;

namespace Systems.OrderSortingByPositionY
{
    public class OrderSortingByPositionYController
    {
        public readonly OrderSortingByPositionYModel Model;

        public OrderSortingByPositionYController(MonoBehaviourObserver monoBehaviourObserver,
            OrderSortingByPositionYModel model)
        {
            Model = model;
            monoBehaviourObserver.Updated += Sort;
        }

        private void Sort()
        {
            Model.Transforms = Model.Transforms.OrderBy(transform => transform.position.y).ToList();

            var count = Model.Transforms.Count;
            for (var i = 0; i < count; i++)
            {
                var index = i;
                var position = Model.Transforms[index].position;
                Model.Transforms[index].transform.position = new Vector3(position.x, position.y, i);
            }
        }
    }
}