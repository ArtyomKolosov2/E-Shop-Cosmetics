export function buildSlider(priceMinRange, priceMaxRange, priceMin, priceMax, slider) {
    slider.ionRangeSlider({
        type: "double",
        grid: true,
        min: priceMinRange,
        max: priceMaxRange,
        from: priceMinRange,
        to: priceMinRange + 5,
        step: 1,
        postfix: "br",
        onChange: function (data) {
            priceMin = data.from_pretty;
            priceMax = data.to_pretty;
        },

    });
    const editPriceMin = () => priceMin = slider.from;

    const editPriceMax = () => priceMax = slider.to;

    // priceMin.addEventListener("input", editPriceMin);

    // priceMax.addEventListener("input", editPriceMax);
}