function eventModalCart() {
    modalCart.classList.toggle("on-click");
}

const cardBtn = document.querySelector("#btn-basket");
const modalCart = document.querySelector(".modal-cart");
const close = document.querySelector(".btn-close");
const cancel = document.querySelector(".btn-cancel");

//modal cart
cardBtn.addEventListener("click", eventModalCart);

close.addEventListener("click", eventModalCart);

cancel.addEventListener("click", eventModalCart);

// modal search
function eventModalFilter() {
    modalSearch.classList.toggle("on-click");
}
const filterBtn = document.querySelector('#btn-filter');
const modalSearch = document.querySelector(".modal-search");
const closeFilter = document.querySelector(".btn-close-filter");
const cancelFilter = document.querySelector(".btn-cancel-filter");

filterBtn.addEventListener("click", eventModalFilter);

closeFilter.addEventListener("click", eventModalFilter);

cancelFilter.addEventListener("click", eventModalFilter);

// slider for filter
const priceMin = document.getElementById("priceMin");
const priceMax = document.getElementById("priceMax");
const $data = $(".js-range-slider");

/*priceMin.addEventListener("change", priceMin.value = $data.from);
priceMax.addEventListener("change", priceMax.value = $data.to);*/

$data.ionRangeSlider({
    type: "double",
    grid: true,
    min: 1,
    max: 1000,
    postfix: "br",
    onStart: function (data) {
        data.from = document.getElementById("priceMin").value;
        data.to = document.getElementById("priceMin").value;
    },
    onChange: function (data) {
        document.getElementById("priceMin").value = data.from_pretty;
        document.getElementById("priceMax").value = data.to_pretty;
    },

});
//!slider for filter

//длинна анимации
new WOW().init();