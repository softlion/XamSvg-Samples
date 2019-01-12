using System.Collections.Generic;
using Android.App;
using Android.Content;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace XamSvgTests
{
	[Activity (Label = "Collection")]
	public class CollectionActivity : Activity
	{
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
			SetContentView (Resource.Layout.collection);
            //ActionBar.SetDisplayHomeAsUpEnabled(true);

            var list = FindViewById<ListView>(Resource.Id.list);
            list.Adapter = new CollectionAdapter(this, Resource.Layout.collection_item, new List<CollectionItem>
            {
                new CollectionItem(),
                new CollectionItem(),
                new CollectionItem(),
                new CollectionItem(),
                new CollectionItem(),
                new CollectionItem(),
            });
		}
	}

    public class CollectionAdapter : BaseAdapter<CollectionItem>
    {
        private readonly int layoutId;
        private readonly IList<CollectionItem> items;
        private readonly LayoutInflater inflater;

        public CollectionAdapter(Context context, int layoutId, IList<CollectionItem> items)
        {
            this.layoutId = layoutId;
            this.items = items;
            inflater = (LayoutInflater) context.GetSystemService(Context.LayoutInflaterService);
        }


        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var view = convertView ?? inflater.Inflate(layoutId, null);
            return view;
        }

        public override long GetItemId(int position) => position;
        public override int Count => items.Count;
        public override CollectionItem this[int position] => items[position];
    }

    public class CollectionItem
    {
    }
}
