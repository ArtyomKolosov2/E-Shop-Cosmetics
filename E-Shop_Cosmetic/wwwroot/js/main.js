import { modalCart } from "./modules/modalCart.js";
import { modalFilter } from "./modules/modalFilter.js";

import { getMinPrice, getMaxPrice, setMinMaxPrice } from "./modules/getDataFromDB.js";
import { setCookie, deleteCookie, getCookie } from "./modules/getDataFromCookie.js";
import { addToCart, uploadCart, cartHandler } from "./modules/cart.js";

import { buildSlider } from "./modules/slider.js";
import makingOrder from "./modules/makingOrder.js";

import { cropText, cropTextArray } from "./modules/cropText.js";

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

    uploadCart(getCookie);
    cartHandler(getCookie, setCookie);

    const btnAddProduct = document.getElementById('btn_add_product');
    if (btnAddProduct)
    {
        btnAddProduct.addEventListener('click', function () {
            addToCart(getCookie, setCookie);
        });
    }
    //!basket

    // making order
    const order = document.getElementById('order');
    if (order) {
        document.getElementById("btn-basket").remove();
        await makingOrder(getCookie);
    }
    // !making order

    // crop text
    const textSmall = document.querySelectorAll("#card-info__name");
    const endCharacter = '...';
    if (textSmall) {
        cropTextArray(textSmall, 20, endCharacter);
    }

    const productInfoHeader = document.querySelector(".product-info__header");
    if (productInfoHeader) {
        cropText(productInfoHeader, 50, endCharacter);
    }

    const infoBody = document.querySelector("#offer-info__description__body");
    if (infoBody) {
        cropText(infoBody, 400, endCharacter);
    }
    // !crop text

    //длинна анимации
    new WOW().init();
}

main();