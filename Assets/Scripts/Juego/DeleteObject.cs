using UnityEngine;

public class DeleteObject : MonoBehaviour
{
	/*
	 * Eliminar alimentos fuera de alcance
	 */
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.tag == "Food")
		{
			Destroy(collision.gameObject);
			Debug.Log("Objeto eliminado: " + collision.gameObject.name);
		}
	}
}
