import { modalCart } from "./modules/modalCart.js";
import { modalFilter } from "./modules/modalFilter.js";
import { getMinPrice, getMaxPrice, setMinMaxPrice } from "./modules/getDataFromDB.js";
import { makingOrder, addToCart, removeFromCart, uploadCart, cartHandler } from "./modules/cart.js";
import { buildSlider } from "./modules/slider.js";
//import {  } from "./modules/getDataFromCookie.js";

async function main() {
    
    modalCart();

    // slider for filter
    const filterBtn = document.querySelector('#btn-filter');
    if (filterBtn) {
        modalFilter(filterBtn);

        setMinMaxPrice();

        const maxPrice = await getMaxPrice();
        const minPrice = await getMinPrice();

        const $slider = $(".js-range-slider");

        buildSlider(minPrice, maxPrice, $slider);
    }
    //!slider for filter

    // basket

    uploadCart();
    cartHandler();

    const btnAddProduct = document.getElementById('btn_add_product');
    const btnDelProduct = document.getElementById('btn_del_product');
    if (btnAddProduct && btnDelProduct)
    {
        btnAddProduct.addEventListener('click', function () {
            addToCart(btnAddProduct);
        });
        btnDelProduct.addEventListener('click', function () {
            removeFromCart(btnDelProduct);
        });
    }
    //!basket

    // making order
    const order = document.getElementById('order');
    if (order) {
        makingOrder();
    }
    // !making order

    //длинна анимации
    new WOW().init();
}

main();