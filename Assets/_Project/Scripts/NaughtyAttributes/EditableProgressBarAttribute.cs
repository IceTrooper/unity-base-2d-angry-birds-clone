using System;

namespace NaughtyAttributes
{
	[AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
	public class EditableProgressBarAttribute : DrawerAttribute
	{
		public string Name { get; private set; }
		public float MaxValue { get; set; }
		public string MaxValueName { get; private set; }
		public EColor Color { get; private set; }

		public EditableProgressBarAttribute(string name, float maxValue, EColor color = EColor.Blue)
		{
			Name = name;
			MaxValue = maxValue;
			Color = color;
		}

		public EditableProgressBarAttribute(string name, string maxValueName, EColor color = EColor.Blue)
		{
			Name = name;
			MaxValueName = maxValueName;
			Color = color;
		}

		public EditableProgressBarAttribute(float maxValue, EColor color = EColor.Blue)
			: this("", maxValue, color)
		{
		}

		public EditableProgressBarAttribute(string maxValueName, EColor color = EColor.Blue)
			: this("", maxValueName, color)
		{
		}
	}
}
