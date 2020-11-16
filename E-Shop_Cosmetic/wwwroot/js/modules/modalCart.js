export function modalCart() {
    function eventModalCart() {
        modalCart.classList.toggle("on-click");
    }
    const cardBtn = document.querySelector("#btn-basket");
    const modalCart = document.querySelector(".modal-cart");
    const close = document.querySelector(".btn-close");
    const cancel = document.querySelector(".btn-cancel");

    cardBtn.addEventListener("click", eventModalCart);

    close.addEventListener("click", eventModalCart);

    cancel.addEventListener("click", eventModalCart);
}
