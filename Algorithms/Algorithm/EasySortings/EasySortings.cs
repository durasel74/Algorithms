using System;
using System.Windows;


using System.Windows.Threading;




namespace Algorithms.Algorithm.EasySortings
{
	public class EasySortings : AlgorithmsProcessor
	{
		private string algorithmName = "Сортировка";
		private int sequenceInterval = 1;

		public int Low { get; private set; }
		public int High { get; private set; }

		public int SequenceInterval
		{ 
			get { return sequenceInterval; }
			set
			{
				sequenceInterval = value;
				OnPropertyChanged("SequenceInterval");
			}
		}


		public EasySortings()
		{
			AlgorithmEventHandler += Sort;
			RestartEventHandler += RestartSorting;

			Low = 0;
			High = Array.Count - 1;
		}

		/// <summary>
		/// Представляет свою версию обновления информации для алгоритма. 
		///	(Простая сортировка)
		/// </summary>
		public override void UpdateInfo()
		{
			string info = "" +
			$"Алгоритм: {algorithmName}\n" +
			$"Состояние: {GetState()}\n" +
			$"Количество элементов: {Array.Count}\n" +
			$"Попытка: {Attempt}\n";
			AlgorithmInfo = info;
		}

		/// <summary>
		/// Представляет свою версию обновления массива. (Простая сортировка)
		/// </summary>
		protected override void UpdateArray()
		{
			Array.Clear();
			for (int i = CountFrom; i < CountTo * sequenceInterval + 1; 
				i += sequenceInterval)
			{
				Array.Add(i);
			}
		}

		// Перемешивает массив
		private void ShuffleArray()
		{
			Random random = new Random();
			int j, temp;
			for (int i = Array.Count - 1; i >= 1; i--)
			{
				j = random.Next(i + 1);
				temp = Array[j];
				Array[j] = Array[i];
				Array[i] = temp;
			}
		}

		private void Sort()
		{
			BubbleSort();


		}


		private void BubbleSort()
		{
			
			for (int i = 0; i < Array.Count; i++)
			{
				for (int j = 1; j < Array.Count; j++)
				{
					if (Array[j - 1] > Array[j])
					{
						SwapElementsInArray(j - 1, j);
					}
				}
			}

			//while (!IsComplite)
			//{
			//	if (!TimeManagement()) break;



			//}
		}


		private void SwapElementsInArray(int indexFirst, int indexSecond)
		{
			int temp = Array[indexFirst];
			Application.Current.Dispatcher.Invoke((Action)delegate
			{
				Array[indexFirst] = Array[indexSecond];
				Array[indexSecond] = temp;
			});
		}

		//private void BinSearch()
		//{
		//	int selectedElement;
		//	while (!IsComplite)
		//	{
		//		if (!TimeManagement()) break;
		//		Attempt += 1;
		//		mid = (Low + High) / 2;
		//		elementState = mid.ToString();

		//		selectedElement = SelectedElement;
		//		if (!GetArrayElement(mid, ref selectedElement)) return;
		//		SelectedElement = selectedElement;

		//		if (requiredElement == SelectedElement)
		//		{
		//			Low = mid + 1;
		//			High = mid - 1;
		//			ResultIndex = mid;
		//			elementState = $"Найден ({ResultIndex})";
		//			return;
		//		}
		//		else if (requiredElement > SelectedElement)
		//			Low = mid + 1;
		//		else if (requiredElement < SelectedElement)
		//			High = mid - 1;

		//		if (Low > High)
		//		{
		//			elementState = $"Не найден";
		//			return;
		//		}
		//	}
		//}


		private void RestartSorting()
		{
			//Low = 0;
			//High = Array.Count - 1;
			ShuffleArray();
			UpdateInfo();
		}


	}
}
