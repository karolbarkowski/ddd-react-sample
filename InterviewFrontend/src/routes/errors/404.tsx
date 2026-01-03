import { Empty, EmptyDescription, EmptyHeader, EmptyTitle } from "@/components";

export function RouteNotFound() {
  return (
    <Empty>
      <EmptyHeader>
        <EmptyTitle>404</EmptyTitle>
        <EmptyDescription>Page not found</EmptyDescription>
      </EmptyHeader>
    </Empty>
  );
}
