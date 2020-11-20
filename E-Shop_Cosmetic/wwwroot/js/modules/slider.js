export function buildSlider(priceMinRange, priceMaxRange, priceMin, priceMax, slider) {
    document.getElementById('priceMin').oninput = function () { document.getElementById('priceMin').value = slider.from; render(); }
    document.getElementById('priceMax').oninput = function () { document.getElementById('priceMax').value = slider.to; render(); }
    let priceMin = document.getElementById('priceMin').value;
    let priceMax = document.getElementById('priceMax').value;

    const render = () => slider.ionRangeSlider({
        type: "double",
        grid: true,
        min: priceMinRange,
        max: priceMaxRange,
        from: priceMinRange,
        to: priceMinRange + 10,
        step: 1,
        postfix: "br",
        onStart: function (data) {
            data.min = priceMin;
            data.max = priceMax;
        },
        onChange: function (data) {
            priceMin = data.from_pretty;
            priceMax = data.to_pretty;
        },

    });
    render();
}