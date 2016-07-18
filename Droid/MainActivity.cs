using Android.App;
using Android.Widget;
using Android.OS;

using ObservableTables.ViewModel;

using GalaSoft.MvvmLight.Helpers;
using XamarinItemTouchHelper;
using Android.Support.V7.Widget.Helper;
using Android.Support.V7.Widget;

namespace ObservableTables.Droid
{
	[Activity (Label = "Tasks", Theme = "@style/AppTheme", MainLauncher = true, Icon = "@mipmap/icon")]
	public class MainActivity : Activity, IOnStartDragListener
	{
        private ItemTouchHelper itemTouchHelper;

        private RecyclerView taskRecyclerView;

        private Button addTaskButton;

        public RecyclerView TaskRecyclerView
        {
            get
            {
                return taskRecyclerView ??
                  (taskRecyclerView = FindViewById<RecyclerView>(
                        Resource.Id.tasksRecyclerView));
            }
        }

		public Button AddTaskButton
		{
			get
			{
				return addTaskButton
					?? (addTaskButton = FindViewById<Button>(Resource.Id.addTaskButton));
			}
		}

		public TaskListViewModel Vm
		{
			get
			{
				return App.Locator.TaskList;
			}
		}

		protected override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);

			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Main);

			var toolbar = FindViewById<Toolbar> (Resource.Id.tasksToolbar);
			//Toolbar will now take on default Action Bar characteristics
			SetActionBar (toolbar);

			Vm.Initialize ();
			
            TaskListAdapter adapter = new TaskListAdapter(this);

            TaskRecyclerView.HasFixedSize = true;
            TaskRecyclerView.SetAdapter(adapter);

            TaskRecyclerView.SetLayoutManager(new LinearLayoutManager(this,
                                              LinearLayoutManager.Vertical, false));
            
            ItemTouchHelper.Callback callback = new SimpleItemTouchHelperCallback(adapter);
            itemTouchHelper = new ItemTouchHelper(callback);
            itemTouchHelper.AttachToRecyclerView(TaskRecyclerView);

            //ensure that the Event will be present
            AddTaskButton.Click += (sender, e) => {};

			// Actuate the AddTaskCommand on the VM.
			AddTaskButton.SetCommand("Click",
				                    Vm.AddTaskCommand);
		}

        public void OnStartDrag(RecyclerView.ViewHolder viewHolder)
        {
            itemTouchHelper.StartDrag(viewHolder);
        }
	}
}


