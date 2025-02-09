document.addEventListener("DOMContentLoaded", function () {
    let hero = document.getElementById("hero-container");

    if (hero) {
        window.addEventListener("scroll", function () {
            let scrollY = window.scrollY;
            let opacity = Math.max(1 - scrollY / 300, 0);

            hero.style.opacity = opacity;
            hero.style.transform = translateY(${ scrollY / 4}px);

        if (opacity === 0) {
            hero.style.display = "none";
        } else {
            hero.style.display = "flex";
        }
    });
    }
});