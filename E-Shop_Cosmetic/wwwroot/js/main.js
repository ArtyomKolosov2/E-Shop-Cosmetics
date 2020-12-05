import { modalCart } from "./modules/modalCart.js";
import { modalFilter } from "./modules/modalFilter.js";
import { getMinPrice, getMaxPrice, setMinMaxPrice } from "./modules/getDataFromDB.js";
import { makingOrder, addBasket, basketLogic } from "./modules/basket.js";
import { buildSlider } from "./modules/slider.js";
// import { Product } from "./modules/product.js";
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

    basketLogic();

    const btnAddProduct = document.getElementById('btn_add_product');
    if (btnAddProduct)
    {
        btnAddProduct.addEventListener('click', function () {
            addBasket(btnAddProduct);
        });
    }

    makingOrder();
    //!basket

    //длинна анимации
    new WOW().init();
}

main();