import type { Meta, StoryObj } from '@storybook/react'
import {
  Empty,
  EmptyContent,
  EmptyDescription,
  EmptyHeader,
  EmptyMedia,
  EmptyTitle,
} from './empty'
import { Button } from '../button/button'
import { FileIcon, FolderIcon, InboxIcon, SearchIcon } from 'lucide-react'

const meta = {
  title: 'UI/Empty',
  component: Empty,
  parameters: {
    layout: 'centered',
  },
  tags: ['autodocs'],
} satisfies Meta<typeof Empty>

export default meta
type Story = StoryObj<typeof meta>

export const Default: Story = {
  render: () => (
    <Empty>
      <EmptyHeader>
        <EmptyMedia>
          <InboxIcon className="size-12 text-muted-foreground" />
        </EmptyMedia>
        <EmptyTitle>No items found</EmptyTitle>
        <EmptyDescription>
          There are no items to display at the moment.
        </EmptyDescription>
      </EmptyHeader>
    </Empty>
  ),
}

export const WithIcon: Story = {
  render: () => (
    <Empty>
      <EmptyHeader>
        <EmptyMedia variant="icon">
          <InboxIcon />
        </EmptyMedia>
        <EmptyTitle>No messages</EmptyTitle>
        <EmptyDescription>
          You don't have any messages yet. Start a conversation to see messages here.
        </EmptyDescription>
      </EmptyHeader>
    </Empty>
  ),
}

export const WithAction: Story = {
  render: () => (
    <Empty>
      <EmptyHeader>
        <EmptyMedia variant="icon">
          <FolderIcon />
        </EmptyMedia>
        <EmptyTitle>No projects yet</EmptyTitle>
        <EmptyDescription>
          Get started by creating your first project.
        </EmptyDescription>
      </EmptyHeader>
      <EmptyContent>
        <Button>Create Project</Button>
      </EmptyContent>
    </Empty>
  ),
}

export const SearchResults: Story = {
  render: () => (
    <Empty>
      <EmptyHeader>
        <EmptyMedia variant="icon">
          <SearchIcon />
        </EmptyMedia>
        <EmptyTitle>No results found</EmptyTitle>
        <EmptyDescription>
          We couldn't find any results matching your search. Try adjusting your
          filters or search terms.
        </EmptyDescription>
      </EmptyHeader>
      <EmptyContent>
        <div className="flex gap-2">
          <Button variant="outline">Clear Filters</Button>
          <Button>Browse All</Button>
        </div>
      </EmptyContent>
    </Empty>
  ),
}

export const NoFiles: Story = {
  render: () => (
    <Empty>
      <EmptyHeader>
        <EmptyMedia>
          <FileIcon className="size-16 text-muted-foreground" />
        </EmptyMedia>
        <EmptyTitle>No files uploaded</EmptyTitle>
        <EmptyDescription>
          Upload your first file to get started. Supported formats include PDF,
          DOC, and TXT.
        </EmptyDescription>
      </EmptyHeader>
      <EmptyContent>
        <div className="flex flex-col gap-2">
          <Button>Upload File</Button>
          <Button variant="ghost" size="sm">
            Learn more about file uploads
          </Button>
        </div>
      </EmptyContent>
    </Empty>
  ),
}

export const WithLink: Story = {
  render: () => (
    <Empty>
      <EmptyHeader>
        <EmptyMedia variant="icon">
          <InboxIcon />
        </EmptyMedia>
        <EmptyTitle>Nothing to see here</EmptyTitle>
        <EmptyDescription>
          Your inbox is empty. Check out{' '}
          <a href="#" onClick={(e) => e.preventDefault()}>
            our help center
          </a>{' '}
          to learn more.
        </EmptyDescription>
      </EmptyHeader>
    </Empty>
  ),
}

export const MinimalWithoutIcon: Story = {
  render: () => (
    <Empty>
      <EmptyHeader>
        <EmptyTitle>No data available</EmptyTitle>
        <EmptyDescription>There is no data to display.</EmptyDescription>
      </EmptyHeader>
    </Empty>
  ),
}

export const AllVariants: Story = {
  render: () => (
    <div className="flex flex-col gap-8 max-w-4xl">
      <div>
        <h3 className="text-sm font-medium mb-4">Default Media</h3>
        <Empty className="max-w-md">
          <EmptyHeader>
            <EmptyMedia>
              <InboxIcon className="size-12 text-muted-foreground" />
            </EmptyMedia>
            <EmptyTitle>Default variant</EmptyTitle>
            <EmptyDescription>
              This uses the default media variant with a large icon.
            </EmptyDescription>
          </EmptyHeader>
        </Empty>
      </div>
      <div>
        <h3 className="text-sm font-medium mb-4">Icon Media</h3>
        <Empty className="max-w-md">
          <EmptyHeader>
            <EmptyMedia variant="icon">
              <InboxIcon />
            </EmptyMedia>
            <EmptyTitle>Icon variant</EmptyTitle>
            <EmptyDescription>
              This uses the icon media variant with a background.
            </EmptyDescription>
          </EmptyHeader>
        </Empty>
      </div>
    </div>
  ),
}
