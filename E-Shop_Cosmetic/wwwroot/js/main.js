import { modalCart } from "./modules/modalCart.js";
import { modalFilter } from "./modules/modalFilter.js";
import { GetMinPrice, GetMaxPrice, SetMinMaxPrice } from "./modules/getDataFromDB.js";
import { basketLogic } from "./modules/basket.js";
import { buildSlider } from "./modules/slider.js";

main();

async function main() {
    
    modalCart();

    // slider for filter
    
    modalFilter();

    SetMinMaxPrice();

    //!slider for filter
    const maxPrice = await GetMaxPrice();
    const minPrice = await GetMinPrice();

    const $slider = $(".js-range-slider");

    buildSlider(minPrice, maxPrice, $slider);

    // basket
    
    basketLogic();

    //!basket

    //длинна анимации
    new WOW().init();
}
