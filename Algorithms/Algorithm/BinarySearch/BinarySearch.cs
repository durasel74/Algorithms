using System;

namespace Algorithms.Algorithm.BinarySearch
{
	/// <summary>
	/// Алгоритм бинарного поиска в списке.
	/// </summary>
	public class BinarySearch : AlgorithmsProcessor
	{
		private string algorithmName = "Бинарный поиск";

		private int requiredElement;
		private int mid;
		private string elementState;

		public int Low { get; private set; }
		public int High { get; private set; }
		public int ResultIndex { get; private set; }

		public int RequiredElement
		{ 
			get { return requiredElement; }
			set
			{
				requiredElement = CountLimitCheck(value);
				OnPropertyChanged("FindElement");
			}
		}

		public BinarySearch()
		{
			AlgorithmEventHandler += BinSearch;
			RestartEventHandler += RestartSearch;

			Low = 0;
			High = Array.Count - 1;
			ResultIndex = -1;
			RequiredElement = 48;
		}

		/// <summary>
		/// Представляет свою версию обновления информации для алгоритма. 
		///	(Поиск в массиве)
		/// </summary>
		public override void UpdateInfo()
		{
			string info = "" +
			$"Алгоритм: {algorithmName}\n" +
			$"Состояние: {GetState()}\n" +
			$"Количество элементов: {Array.Count}\n" +
			$"Попытка: {Attempt}\n" +
			$"Индекс: {elementState}";
			AlgorithmInfo = info;
		}

		private void BinSearch()
		{
			int selectedElement;
			while (!IsComplite)
			{
				if (!TimeManagement()) break;
				Attempt += 1;
				mid = (Low + High) / 2;
				elementState = mid.ToString();

				selectedElement = SelectedElement;
				if (!GetArrayElement(mid, ref selectedElement)) return;
				SelectedElement = selectedElement;

				if (requiredElement == SelectedElement)
				{
					Low = mid + 1;
					High = mid - 1;
					ResultIndex = mid;
					elementState = $"Найден ({ResultIndex})";
					return;
				}
				else if (requiredElement > SelectedElement)
					Low = mid + 1;
				else if (requiredElement < SelectedElement)
					High = mid - 1;

				if (Low > High)
				{
					elementState = $"Не найден";
					return;
				}
			}
		}
		private void RestartSearch()
		{
			ResultIndex = -1;
			Low = 0;
			High = Array.Count - 1;
			elementState = "";
			UpdateInfo();
		}
    }
}
