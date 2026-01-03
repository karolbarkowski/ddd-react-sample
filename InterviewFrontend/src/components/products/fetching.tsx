import { Spinner } from "@/components";

export function FetchingProducts() {
  return (
    <div className="flex items-center justify-center w-full flex-col gap-4">
      <Spinner className="size-8" />
      <span className="text-xs">fetching...</span>
    </div>
  );
}
