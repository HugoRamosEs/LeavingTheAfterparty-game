let theme;
const html = document.querySelector("html");
const icon = document.querySelector(".icon");

console.log("hola");

const applyTheme = () => {
  if (theme === "dark") {
    html.classList.add("dark");
    html.classList.remove("light");
  } else {
    html.classList.add("light");
    html.classList.remove("dark");
  }
};

const loadTheme = () => {
  theme = localStorage.getItem("theme");
  if (theme === null) {
    theme = "dark";
  }
  applyTheme();
};

loadTheme();

icon.addEventListener("click", () => {
  if (theme === "dark") {
    theme = "light";
  } else {
    theme = "dark";
  }

  applyTheme();
  localStorage.setItem("theme", theme);
});
