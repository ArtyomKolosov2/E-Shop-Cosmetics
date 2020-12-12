export function modalCart() {
    function eventModalCart() {
        modalCart.classList.toggle("on-click");
    }
    const cardBtn = document.querySelector("#btn-basket");
    const modalCart = document.querySelector(".modal-cart");
    const close = document.querySelector(".btn-close");

    cardBtn.addEventListener("click", eventModalCart);

    close.addEventListener("click", eventModalCart);
}
