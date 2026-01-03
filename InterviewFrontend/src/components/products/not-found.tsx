import { Empty, EmptyDescription, EmptyHeader, EmptyTitle } from "@/components";

export function ProductsNotFound() {
  return (
    <Empty>
      <EmptyHeader>
        <EmptyTitle>No Products Found</EmptyTitle>
        <EmptyDescription>Please come back later.</EmptyDescription>
      </EmptyHeader>
    </Empty>
  );
}
