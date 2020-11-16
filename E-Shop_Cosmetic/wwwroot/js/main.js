import { modalCart } from "./modules/modalCart.js";
modalCart();

import { modalFilter } from "./modules/modalFilter.js";
modalFilter();

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
    onStart: function(data)
    {
        data.from = document.getElementById("priceMin").value;
        data.to = document.getElementById("priceMax").value;
        data.min = document.getElementById("priceMin").value;
        data.max = document.getElementById("priceMax").value;
    },
    onChange: function(data) {
        document.getElementById("priceMin").value = data.from_pretty;
        document.getElementById("priceMax").value = data.to_pretty;
    },

});
//!slider for filter

//длинна анимации
new WOW().init();