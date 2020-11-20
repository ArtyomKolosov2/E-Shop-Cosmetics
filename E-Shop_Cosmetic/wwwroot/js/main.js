import { modalCart } from "./modules/modalCart.js";
modalCart();

// slider for filter
import { modalFilter } from "./modules/modalFilter.js";
modalFilter();

import { GetMinPrice, GetMaxPrice } from "./modules/getDataFromDB.js";

const priceMinRange = GetMinPrice();
const priceMaxRange = GetMaxPrice();
const slider = $(".js-range-slider");

import { buildSlider } from "./modules/slider.js";

buildSlider(priceMinRange, priceMaxRange, slider);
//!slider for filter

// basket
import { basketLogic } from "./modules/basket.js";
basketLogic();

//!basket

//длинна анимации
new WOW().init();