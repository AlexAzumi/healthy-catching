using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodType : MonoBehaviour
{
	[Header("Tipo de comida")]
	public string foodType;

	/*
	 * Obtener el tipo de comida
	 */
	public string getFoodType()
	{
		return foodType;
	}
}
