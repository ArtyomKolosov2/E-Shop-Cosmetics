import { modalCart } from "./modules/modalCart.js";
modalCart();

// slider for filter
import { modalFilter } from "./modules/modalFilter.js";
modalFilter();

// import { GetMinPrice, GetMaxPrice } from "./modules/getDataFromDB.js";
async function GetMinPrice() {
    const response = await fetch("/api/ProductsApi/MinMaxPrice", {
        method: "GET",
        headers: { "Accept": "application/json" }
    });
    if (response.ok === true) {
        const user = await response.json();
        const form = document.forms["form_search"];
        form.elements["priceMin"].value = user.min;
        return Number(user.min);
    } else {
        return 0;
    }
}
async function GetMaxPrice() {
    const response = await fetch("/api/ProductsApi/MinMaxPrice", {
        method: "GET",
        headers: { "Accept": "application/json" }
    });
    if (response.ok === true) {
        const user = await response.json();
        const form = document.forms["form_search"];
        form.elements["priceMax"].value = user.max;
        return Number(user.max);
    } else {
        return 0;
    }
}
const priceMinRange = GetMinPrice();
const priceMaxRange = GetMaxPrice();
let priceMin = 0, priceMax = 0;
const slider = $(".js-range-slider");

import { buildSlider } from "./modules/slider.js";

buildSlider(priceMinRange, priceMaxRange, priceMin, priceMax, slider);
//!slider for filter

// basket
import { basketLogic } from "./modules/basket.js";
basketLogic();

//!basket

//длинна анимации
new WOW().init();