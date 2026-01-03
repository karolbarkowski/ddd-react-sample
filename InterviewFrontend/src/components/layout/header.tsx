import { Link } from "react-router-dom";

export const Header = () => {
  return (
    <header className="container mx-auto flex py-4 items- space-around">
      <Link to="/" className="flex flex-row gap-4 flex-1 items-center">
        <img data-testid="logo-img" src="/react.svg" className="max-w-12" />
        <h1 className="text-xl font-semibold">React Sample App</h1>
      </Link>
    </header>
  );
};
