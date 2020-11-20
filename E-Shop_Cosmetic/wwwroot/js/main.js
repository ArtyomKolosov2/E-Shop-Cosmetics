import { modalCart } from "./modules/modalCart.js";
import { modalFilter } from "./modules/modalFilter.js";
import { getMinPrice, getMaxPrice, setMinMaxPrice } from "./modules/getDataFromDB.js";
import { basketLogic } from "./modules/basket.js";
import { buildSlider } from "./modules/slider.js";
import { autorized } from "./modules/getDataFromCookie.js";

async function main() {
    
    modalCart();

    // slider for filter
    
    modalFilter();

    setMinMaxPrice();

    const maxPrice = await getMaxPrice();
    const minPrice = await getMinPrice();

    const $slider = $(".js-range-slider");

    buildSlider(minPrice, maxPrice, $slider);
    //!slider for filter

    // basket

    basketLogic();

    //!basket

    //длинна анимации
    new WOW().init();
}

main();