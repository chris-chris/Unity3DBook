using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Text;

public class CollectionTest : MonoBehaviour {

	public Text textResult;

	public void ArrayTest()
	{
		int[] exp = new int[10];
		exp[0] = 0;
		exp[1] = 100;
		exp[2] = 300;
		exp[3] = 800;
		exp[4] = 1500;
		exp[5] = 3200;
		exp[6] = 6800;
		exp[7] = 15000;
		exp[8] = 27000;
		exp[9] = 55000;

		textResult.text = "[ArrayTest] Required Exp for Level 4 : " + exp[4];

	}

	public void DArrayTest()
	{
		ArrayList list = new ArrayList();
		list.Add(0);
		list.Add(100);
		list.Add(300);
		list.Add(800);
		list.Add(1500);
		list.Add(3200);
		list.Add(6800);
		list.Add(15000);
		list.Add(27000);
		list.Add(55000);
		
		textResult.text = "[DArrayTest] Required Exp for Level 4 : " + list[4];

	}

	public void LinkedListTest()
	{
		LinkedList<string> linked = new LinkedList<string>();
		linked.AddLast("Monkey");
		linked.AddLast("Cow");
		linked.AddLast("Mouse");

		linked.AddAfter(linked.Find("Monkey"), "Chiken");

		StringBuilder str = new StringBuilder();

		foreach(string one in linked)
		{
			str.Append(one);
			str.Append("\n");
		}

		textResult.text = str.ToString();

	}

	public void DictionaryTest()
	{
		Dictionary<string, int> items = new Dictionary<string, int>();
		items.Add("potion", 3);
		items.Add("booster", 2);
		items.Add("ticket", 1);

		StringBuilder str = new StringBuilder();

		foreach(var item in items)
		{
			str.Append("Key: " + item.Key + " / Value : " + item.Value + "\n");
		}

		textResult.text = str.ToString();

	}

	public void HashTableTest()
	{
		Hashtable table = new Hashtable();

		table.Add("potion", 3);
		table.Add("booster", 4);
		table.Add("ticket", 2);
		
		StringBuilder str = new StringBuilder();

		foreach(DictionaryEntry item in table)
		{
			str.Append("Key: " + item.Key + " / Value : " + item.Value + "\n");
		}
		
		textResult.text = str.ToString();
	}

	public class Friend{
		public string name;
		public int level;
		public int point;
		public int rank;
		public Friend(string _name, int _level, int _point, int _rank)
		{
			name = _name;
			level = _level;
			point = _point;
			rank = _rank;
		}
	}

	// No duplicate entries
	public void HashSetTest()
	{
		Friend chris = new Friend("Chris",10,1230,1);
		Friend john = new Friend("John",5,234,3);
		Friend annie = new Friend("Annie",7,902,2);
		Friend rosy = new Friend("Rosy",1,10,4);

		HashSet<Friend> friend_list = new HashSet<Friend>();
		friend_list.Add(chris);
		friend_list.Add(john);
		friend_list.Add(annie);
		friend_list.Add(rosy);

		StringBuilder str = new StringBuilder();

		foreach(Friend friend in friend_list)
		{
			str.Append("Name: ");str.Append(friend.name);
			str.Append(" / ");
			str.Append("Level: ");str.Append(friend.level);
			str.Append(" / ");
			str.Append("Point: ");str.Append(friend.point);
			str.Append(" / ");
			str.Append("Rank: ");str.Append(friend.rank);
			str.Append("\n");
		}
		textResult.text = str.ToString();
	}


	public void StringBuilderTest()
	{
		float timeStart = Time.time;
		System.DateTime StartTime = System.DateTime.Now;

		string s = "";

		for(int i = 0 ; i < 10000 ; i++)
		{
			s += "Data inserting!";
		}
		long timeStrPassed = (System.DateTime.Now - StartTime).Ticks;

		StartTime = System.DateTime.Now;

		StringBuilder str = new StringBuilder();
		for(int i = 0 ; i < 10000 ; i++)
		{
			str.Append("Data inserting!");
		}
		
		long timeSBPassed = (System.DateTime.Now - StartTime).Ticks;
	
		textResult.text = "String str += 10,000 times : " + timeStrPassed + " ticks!\n" 
			+ "StringBuilder .Append() 10,000 times : " + timeSBPassed + " ticks!";


	}

}
