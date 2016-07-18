using System.Collections.Specialized;
using Android.Support.V7.Widget;
using Android.Views;
using ObservableTables.ViewModel;
using XamarinItemTouchHelper;

namespace ObservableTables.Droid
{
    public class TaskListAdapter : RecyclerView.Adapter, IItemTouchHelperAdapter
    {
        public TaskListViewModel Vm
        {
            get
            {
                return App.Locator.TaskList;
            }
        }

        private IOnStartDragListener dragStartListener;

        public TaskListAdapter(IOnStartDragListener dragStartListener)
        {
            this.dragStartListener = dragStartListener;

            Vm.TodoTasks.CollectionChanged += NotifierCollectionChanged;
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            View view = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.TaskTemplate, parent, false);

            TaskViewHolder taskViewHolder = new TaskViewHolder(view);

            return taskViewHolder;
        }

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            var taskHolder = (TaskViewHolder)holder;

            var task = Vm.TodoTasks[position];
            taskHolder.NameTextView.Text = task.Name;
            taskHolder.NotesTextView.Text = task.Notes;
            
            //taskHolder.NameTextView.SetOnTouchListener(new TouchListenerHelper(taskHolder, dragStartListener));

            taskHolder.HandleView.SetOnTouchListener(new TouchListenerHelper(taskHolder, dragStartListener));
        }

        public void OnItemDismiss(int position)
        {
            Vm.TodoTasks.RemoveAt(position);

            NotifyItemRemoved(position);
        }

        public bool OnItemMove(int fromPosition, int toPosition)
        {
            Vm.TodoTasks.Move(fromPosition, toPosition);

            NotifyItemMoved(fromPosition, toPosition);

            return true;
        }

        private void NotifierCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            NotifyDataSetChanged();
        }

        public override int ItemCount
        {
            get
            {
                return Vm.TodoTasks.Count;
            }
        }
    }
}

