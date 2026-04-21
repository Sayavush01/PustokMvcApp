$(document).ready(function () {
    $(document).on("click", ".bookModalIcon", function (e) {
        e.preventDefault();
        let url = $(this).attr("href");
        fetch(url)
            .then((response) => response.text())
            .then((data) => {
                $("#quickModal .modal-dialog").html(data);

                let firstSlider = {
                    "slidesToShow": 1,
                    "arrows": false,
                    "fade": true,
                    "draggable": false,
                    "swipe": false,
                    "asNavFor": ".product-slider-nav"

                };
                let secondSlider = {
                    "infinite": true,
                    "autoplay": true,
                    "autoplaySpeed": 8000,
                    "slidesToShow": 4,
                    "arrows": true,
                    "prevArrow": { "buttonClass": "slick-prev", "iconClass": "fa fa-chevron-left" },
                    "nextArrow": { "buttonClass": "slick-next", "iconClass": "fa fa-chevron-right" },
                    "asNavFor": ".product-details-slider",
                    "focusOnSelect": true
                }
                $(".product-details-slider").slick(firstSlider);
                $(".product-slider-nav").slick(secondSlider);
                $("#quickModal").modal('show');
            });
    });
    $(document).on("click", ".addbasket, .addToCart", function (e) {
        e.preventDefault();
        let url = $(this).attr("href");
        fetch(url)
            .then((response) => response.text())
            .then((html) => {
                $(".cart-dropdown-block").html(html);
            });
    });

    $('a[data-bs-toggle="tab"]').on('shown.bs.tab', function (e) {
        $('.sb-slick-slider').slick('setPosition');
    });

});
