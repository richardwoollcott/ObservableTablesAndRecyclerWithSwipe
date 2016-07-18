using Android.Graphics;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using XamarinItemTouchHelper;

namespace ObservableTables.Droid
{
    /// <summary>
    /// Simple example of a view holder that implements ItemTouchHelperViewHolder and has a
    /// "handle" view that initiates a drag event when touched.
    /// </summary>
    public class TaskViewHolder : RecyclerView.ViewHolder, IItemTouchHelperViewHolder
    {
        public TextView NameTextView { get; set;}
        public TextView NotesTextView { get; set; }
        public View itemView;

        public TaskViewHolder(View itemView) : base(itemView)
        {
            this.itemView = itemView;
            NameTextView = itemView.FindViewById<TextView>(Resource.Id.NameTextView);
            NotesTextView = itemView.FindViewById<TextView>(Resource.Id.NotesTextView);
        }

        public void OnItemSelected()
        {
            itemView.SetBackgroundColor(Color.LightGray);
        }

        public void OnItemClear()
        {
            itemView.SetBackgroundColor(Color.White);
        }
    }
}

