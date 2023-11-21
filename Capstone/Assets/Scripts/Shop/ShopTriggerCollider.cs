using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopTriggerCollider : MonoBehaviour {
    [SerializeField] private UIShop Shop;
    [SerializeField] private Animator animator;

    private bool Istalking = false;

    public void EnterShop() {
        var Player = GameObject.FindGameObjectWithTag("Player");
        var Templates = GameObject.FindGameObjectWithTag("Player").GetComponent<IShopCustomer>();
        var Direction = (Player.transform.position - transform.position).normalized;

        Shop.Show(Templates);

        if (!Istalking) {
            animator.SetFloat("XDirection", Direction.x);
            animator.SetFloat("YDirection", Direction.y);
            animator.SetTrigger("ShopEnter");
        }
        Istalking = true;
    }

    public void ExitShop() {
        var Player = GameObject.FindGameObjectWithTag("Player");
        var Direction = (Player.transform.position - transform.position).normalized;

        Shop.Hide();

        if (Istalking) {
            animator.SetFloat("XDirection", Direction.x);
            animator.SetFloat("YDirection", Direction.y);
            animator.SetTrigger("ShopExit");
        }
        Istalking = false;
    }

    public void OnTriggerExit2D(Collider2D collision) {
        if (collision.TryGetComponent<IShopCustomer>(out _)) {
            var Direction = (collision.transform.position - transform.position).normalized;

            Shop.Hide();

            if (Istalking) {
                animator.SetFloat("XDirection", Direction.x);
                animator.SetFloat("YDirection", Direction.y);
                animator.SetTrigger("ShopExit");
            }
        }
        Istalking = false;
    }
}